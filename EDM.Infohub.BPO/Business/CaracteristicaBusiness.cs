using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Processamento.Impl.Cadastro;
using EDM.Infohub.BPO.Processamento.Impl.Preco;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Business
{
    public class CaracteristicaBusiness
    {

        private readonly ILuzService _luzService;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IConfiguration _config;
        private HangfireService _hangfire;
        private RastreamentoMessenger _rastreamentoMessenger;

        public CaracteristicaBusiness(IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, DadosCaracteristicosRepository dadosCaracteristicos, ISender rabbitSender, HangfireService hangfire, RastreamentoMessenger rastreamentoMessenger)
        {
            _luzService = luzService;
            _rawData = rawData;
            _mapper = mapper;
            _dadosCaracteristicos = dadosCaracteristicos;
            _rabbitSender = rabbitSender;
            _assinatura = assinaturaRepository;
            _config = config;
            _hangfire = hangfire;
            _rastreamentoMessenger = rastreamentoMessenger;
        }
        public List<DadosCaracteristicos> RelatorioCaracteristica(DateTime data, int tentativa, string user)
        {
            var cadastroHandler = new CadastroHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosCaracteristicos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<DadosCaracteristicos>>(_rawData, _mapper);
            var cadastroPersistenceHandler = new CadastroPersistenceHandler(_dadosCaracteristicos, _assinatura, _mapper, _rastreamentoMessenger);
            var cadastroImpactHandler = new CadastroImpactHandler(_rabbitSender, _mapper, _rastreamentoMessenger);

            cadastroHandler.SetNext(tentativaHandler)
                    .SetNext(rawDataHandler)
                    .SetNext(cadastroPersistenceHandler)
                    .SetNext(cadastroImpactHandler);

            tentativaHandler.RetryUrl = "v2/Caracteristica/relatorio-dia/";

            List<DadosCaracteristicos> relatorioCompleto = new List<DadosCaracteristicos>();
            List<DadosCaracteristicos> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.CADASTRO, null, "/v2/Caracteristica/relatorio-dia", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            do
            {
                (relatorio, headers) = _luzService.RelatorioCaracteristica(data, hasNext).Result;                

                tentativaHandler.Tentativa = tentativa;
                cadastroImpactHandler.Data = data;
                cadastroHandler.Handle(relatorio, EProcessStep.MDP);

                relatorioCompleto.AddRange(relatorio);

                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;
            } while (hasNext != "");

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }
        public (List<DadosCaracteristicos>, List<object>) RelatorioCaracteristicaPapel(DateTime data, string papel, int tentativa, string user)
        {
            var cadastroHandler = new CadastroHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosCaracteristicos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<DadosCaracteristicos>>(_rawData, _mapper);
            var cadastroPersistenceHandler = new CadastroPersistenceHandler(_dadosCaracteristicos, _assinatura, _mapper, _rastreamentoMessenger, true);
            var cadastroImpactHandler = new CadastroImpactHandler(_rabbitSender, _mapper, _rastreamentoMessenger);

            cadastroHandler.SetNext(tentativaHandler)
                    .SetNext(rawDataHandler)
                    .SetNext(cadastroPersistenceHandler)
                    .SetNext(cadastroImpactHandler);

            tentativaHandler.RetryUrl = $"v2/Caracteristica/relatorio-dia/{papel}";

            List<DadosCaracteristicos> relatorio = null;

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.CADASTRO, null, $"v2/Caracteristica/relatorio-dia/{papel}", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            relatorio = _luzService.RelatorioCaracteristicaPapel(data, papel).Result;

            tentativaHandler.Tentativa = tentativa;
            cadastroImpactHandler.Data = data;
            cadastroHandler.Handle(relatorio, EProcessStep.MDP);

            var _messages = new List<object>();
            _messages.Add(new { message = _rastreamentoMessenger.HouveEnvioMDP ? $"O papel {papel} foi modificado no MDP" : $"O papel {papel} não foi modificado no MDP"});
            _messages.Add(new { message = _rastreamentoMessenger.HouvePersistenciaBPO ? $"O papel {papel} foi modificado no BPO" : $"O papel {papel} não foi modificado no BPO" });

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return (relatorio,_messages);
        }

        public List<DadosCaracteristicos> RecoveryAll(DateTime data, string user)
        {
            var precoHandler = new CadastroHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosCaracteristicos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            //var rawDataHandler = new RawDataHandler<List<DadosCaracteristicos>>(_rawData, _mapper);
            //var cadastroPersistence = new CadastroPersistenceHandler(_dadosCaracteristicos, _assinatura, _mapper);
            var precosImpact = new RecoveryImpactHandler(_dadosCaracteristicos, _assinatura, _mapper, _rabbitSender, _rastreamentoMessenger);

            precoHandler.SetNext(tentativaHandler)
                    .SetNext(precosImpact);

            List<DadosCaracteristicos> relatorioCompleto = new List<DadosCaracteristicos>();
            List<DadosCaracteristicos> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";
            precosImpact.Data = data;
            tentativaHandler.RetryUrl = "NORETRY";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.CADASTRO, null, "/v1/Recovery/relatorio-dia", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            do
            {
                (relatorio, headers) = _luzService.RelatorioCaracteristica(data, hasNext).Result;
                tentativaHandler.Tentativa = 1;

                precoHandler.Handle(relatorio, EProcessStep.MDP);

                relatorioCompleto.AddRange(relatorio);

                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;
            } while (hasNext != "");

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }


        public List<DadosCaracteristicos> RecoveryUnitity(DateTime data, string papel, string user)
        {
            var precoHandler = new CadastroHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<DadosCaracteristicos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            //var rawDataHandler = new RawDataHandler<List<DadosCaracteristicos>>(_rawData, _mapper);
            //var cadastroPersistence = new CadastroPersistenceHandler(_dadosCaracteristicos, _assinatura, _mapper);
            var precosImpact = new RecoveryImpactHandler(_dadosCaracteristicos, _assinatura, _mapper, _rabbitSender, _rastreamentoMessenger);

            precoHandler.SetNext(tentativaHandler)
                    .SetNext(precosImpact);

            List<DadosCaracteristicos> relatorio = null;
            precosImpact.Data = data;
            tentativaHandler.RetryUrl = "NORETRY";
            tentativaHandler.Tentativa = 1;

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.CADASTRO, null, "/v1/Recovery/relatorio-dia", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            relatorio = _luzService.RelatorioCaracteristicaPapel(data, papel).Result;

            precoHandler.Handle(relatorio, EProcessStep.MDP);

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorio;
        }
    }
}
