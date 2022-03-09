using AutoMapper;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace EDM.Infohub.BPO.Processamento
{
    public class HandlerImplementation<T> : AbstractProcessamentoHandler<T> where T : IList
    {
        public override T Handle(T request, EProcessStep step)
        {
            return base.Handle(request, step);
        }
    }
    public class ValidadorTentativaHandler<T> : AbstractProcessamentoHandler<T> where T : IList
    {
        private int _tentativa = 0;
        private HangfireService _hangfire;
        private IConfiguration _configuration;
        private RastreamentoMessenger _rastreamentoMessenger;
        private IMapper _mapper;
        private string _retryUrl;
        public string RetryUrl
        {
            get { return _retryUrl; }
            set { _retryUrl = value; }
        }

        public int Tentativa
        {
            get { return _tentativa; }
            set { _tentativa = value; }
        }

        public ValidadorTentativaHandler(IConfiguration configuration, HangfireService hangfire, RastreamentoMessenger rastreamentoMessenger, IMapper mapper)
        {
            _configuration = configuration;
            _hangfire = hangfire;
            _rastreamentoMessenger = rastreamentoMessenger;
            _mapper = mapper;
        }

        public override T Handle(T request, EProcessStep step)
        {
            if(_rastreamentoMessenger.EventoEmAndamento.TipoRequisicao == TipoRequisicaoEnum.CADASTRO.ToString() ||
                _rastreamentoMessenger.EventoEmAndamento.TipoRequisicao == TipoRequisicaoEnum.PRECO.ToString() ||
                _rastreamentoMessenger.EventoEmAndamento.TipoRequisicao == TipoRequisicaoEnum.EVENTO.ToString())
            {
                Processar(request);
            }
            
            if(_rastreamentoMessenger.EventoEmAndamento.TipoRequisicao!= TipoRequisicaoEnum.PRECO_HISTORICO.ToString())
            {
                IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(request), StatusMensagemEnum.RECEBIDO_LUZ, "");
                _rastreamentoMessenger.SendTrackingMessage(papeis);
            }            
            return base.Handle(request, step);
        }

        private void Processar(T request)
        {
            if (request.Count.Equals(0) && _tentativa < 5)
            {
                _tentativa++;
                if(_retryUrl!= "NORETRY")
                {
                    var agendar = _hangfire.AgendaTemporizador($"{_configuration["InfohubAPIUrl"]}{_retryUrl}?tentativa={_tentativa}", 10).Result;
                }                
                //_logger.LogInformation("Não foi encontrado relatório de preços do dia " + data);
                throw new Exception("O relatório da Luz está vazio");
                //return NoContent();
            }
            else if (_tentativa == 5)
            {
                throw new IndexOutOfRangeException($"Tentativas de requerir o relatório excedida(5), contate a Luz Sistemas para entender o atraso");
            }
        }

    }

    public class RawDataHandler<T> : AbstractProcessamentoHandler<T> where T : IList
    {
        private RawDataRepository _rawData;
        private IMapper _mapper;

        public RawDataHandler(RawDataRepository rawData, IMapper mapper)
        {
            _rawData = rawData;
            _mapper = mapper;
        }

        public override T Handle(T request, EProcessStep step)
        {
            _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(request));

            return base.Handle(request, step);
        }
    }

    public class PrecoHistoricoHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {
            return base.Handle(request, step);
        }
    }
    public class PrecoEventoHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {
            return base.Handle(request, step);
        }
    }
}
