using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.DataAccess.Impl;
using EDM.Infohub.BPO.Mappers;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.DadosAtuais;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Processamento.Impl;
using EDM.Infohub.BPO.Processamento.Impl.Preco;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;

namespace EDM.Infohub.BPO.Business
{
    public class PuDeEventoBusiness
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
        private PuDeEventosRepository _puDeEventos;
        private IFireForgetRepositoryHandler _fireForget;

        public PuDeEventoBusiness(IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire, PuDeEventosRepository puDeEventos, RastreamentoMessenger rastreamentoMessenger, IFireForgetRepositoryHandler fireForget)
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
            _puDeEventos = puDeEventos;
            _fireForget = fireForget;
        }

        public async Task<List<PuDeEventos>> RelatorioPuDeEventos(DateTime data, int tentativa, string user)
        {
            var puDeEventoHandler = new PuDeEventosHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<PuDeEventos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<PuDeEventos>>(_rawData, _mapper);
            var puDeEventosPersistence = new PuDeEventosPersistenceHandler(_puDeEventos, _mapper, _rastreamentoMessenger, _assinatura);
            var puDeEventosImpact = new PuDeEventosImpactHandler(_rabbitSender, _assinatura, _rastreamentoMessenger, _mapper, _fireForget);

            puDeEventoHandler.SetNext(tentativaHandler)
                .SetNext(rawDataHandler)
                .SetNext(puDeEventosPersistence)
                .SetNext(puDeEventosImpact);

            tentativaHandler.RetryUrl = "v1/PuDeEventos/relatorio-dia/";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.EVENTO, null, "v1/PuDeEventos/relatorio-dia/", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            List<PuDeEventos> relatorioCompleto = new List<PuDeEventos>();
            List<PuDeEventos> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";

            do
            {
                (relatorio, headers) = _luzService.RelatorioPagamento(data, hasNext).Result;

                tentativaHandler.Tentativa = tentativa;
                puDeEventosPersistence.Data = data;
                puDeEventoHandler.Handle(relatorio, EProcessStep.MDP);

                relatorioCompleto.AddRange(relatorio);

                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;
            } while (hasNext != "");

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }

        public async Task<List<PuDeEventos>> RelatorioPuDeEventosPapel(DateTime data, string papel, int tentativa, string user)
        {
            var puDeEventoHandler = new PuDeEventosHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<PuDeEventos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<PuDeEventos>>(_rawData, _mapper);
            var puDeEventosPersistence = new PuDeEventosPersistenceHandler(_puDeEventos, _mapper, _rastreamentoMessenger, _assinatura);
            var puDeEventosImpact = new PuDeEventosImpactHandler(_rabbitSender, _assinatura, _rastreamentoMessenger, _mapper, _fireForget);

            puDeEventoHandler.SetNext(tentativaHandler)
                .SetNext(rawDataHandler)
                .SetNext(puDeEventosPersistence)
                .SetNext(puDeEventosImpact);

            tentativaHandler.RetryUrl = $"v1/PuDeEventos/{papel}";

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(TipoRequisicaoEnum.EVENTO, null, $"v1/PuDeEventos/{papel}", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            List<PuDeEventos> relatorio = _luzService.RelatorioPagamentoPapel(data, papel).Result;

            tentativaHandler.Tentativa = tentativa;
            puDeEventosPersistence.Data = data;

            puDeEventoHandler.Handle(relatorio, EProcessStep.MDP);

            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorio;
        }
    }
}
