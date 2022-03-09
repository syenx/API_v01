using Amazon.SecretsManager;
using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Processamento.Impl.Preco;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Business
{
    public class PrecoBusiness
    {

        private readonly ILuzService _luzService;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private PrecosRepository _precos;
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IConfiguration _config;
        private HangfireService _hangfire;
        private RastreamentoMessenger _rastreamentoMessenger;
        private IAmazonSecretsManager _secret;
        private DadosCaracteristicosRepository _cadastro;


        public PrecoBusiness(IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire, RastreamentoMessenger rastreamentoMessenger, IAmazonSecretsManager secret, DadosCaracteristicosRepository cadastro)
        {
            _luzService = luzService;
            _rawData = rawData;
            _mapper = mapper;
            _precos = preco;
            _rabbitSender = rabbitSender;
            _assinatura = assinaturaRepository;
            _config = config;
            _hangfire = hangfire;
            _rastreamentoMessenger = rastreamentoMessenger;
            _secret = secret;
            _cadastro = cadastro;
        }
        public List<DadosPrecoLuz> RelatorioPreco(DateTime data, int tentativa, string user)
        {
            var precoHandler = new PrecoHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosPrecoLuz>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<DadosPrecoLuz>>(_rawData, _mapper);
            var precosPersistence = new PrecosPersistenceHandler(_precos, _mapper, _config, _secret, _rastreamentoMessenger);
            var precosImpact = new PrecosImpactHandler(_rabbitSender, _assinatura, _mapper, _rastreamentoMessenger);

            precoHandler.SetNext(tentativaHandler)
                    .SetNext(rawDataHandler)
                    .SetNext(precosPersistence)
                    .SetNext(precosImpact);

            tentativaHandler.RetryUrl = "v2/Preco/relatorio-dia/";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.PRECO, null, "/v2/Preco/relatorio-dia", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);


            List<DadosPrecoLuz> relatorioCompleto = new List<DadosPrecoLuz>();
            List<DadosPrecoLuz> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";

            do
            {
                (relatorio, headers) = _luzService.RelatorioPreco(data, hasNext).Result;

                tentativaHandler.Tentativa = tentativa;
                precosPersistence.Data = data;
                precoHandler.Handle(relatorio, EProcessStep.MDP);

                relatorioCompleto.AddRange(relatorio);

                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;

            } while (hasNext != "");

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }

        public List<DadosPrecoLuz> HistoricoPapel(string papel, int tentativa, string user)
        {
            var precoHandler = new PrecoHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosPrecoLuz>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<DadosPrecoLuz>>(_rawData, _mapper);
            var precosPersistence = new PrecosPersistenceHandler(_precos, _mapper, _config, _secret, _rastreamentoMessenger);
            var precosImpact = new PrecosHistoricoImpactHandler(_rabbitSender, _assinatura, _rastreamentoMessenger, _mapper);

            precoHandler.SetNext(tentativaHandler)
                .SetNext(rawDataHandler)
                .SetNext(precosPersistence)
                .SetNext(precosImpact);

            tentativaHandler.RetryUrl = $"v1/Historico/{papel}";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.PRECO_HISTORICO, null, $"v1/Historico/{papel}", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            List<DadosPrecoLuz> relatorioCompleto = new List<DadosPrecoLuz>();
            List<DadosPrecoLuz> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";

            do
            {
                (relatorio, headers) = _luzService.RelatorioPrecoHistorico(papel, hasNext).Result;

                var relatorioVerificado = VerificaDataInicioRentabilidade(relatorio, papel);
                tentativaHandler.Tentativa = tentativa;

                _ = precoHandler.Handle(relatorioVerificado, EProcessStep.MDP);

                //relatorioCompleto.AddRange(relatorio);

                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;

            } while (hasNext != "");

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }
        
        private List<DadosPrecoLuz> VerificaDataInicioRentabilidade(List<DadosPrecoLuz> relatorio, string papel)
        {
            var dataInicioRentabilidade = _cadastro.DataInicioRentabilidade(Utils.GenerateHash(papel));
            var relatorioVerificado = new List<DadosPrecoLuz>();
            foreach (var pu in relatorio)
            {
                if (pu.DataEvento >= dataInicioRentabilidade)
                    relatorioVerificado.Add(pu);
            }
            return relatorioVerificado;
        }

    }
}
