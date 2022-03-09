using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Processamento.Impl.Fluxo;
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
    public class FluxoBusiness
    {

        private readonly ILuzService _luzService;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private FluxosRepository _fluxos;
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IConfiguration _config;
        private HangfireService _hangfire;
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private RastreamentoMessenger _rastreamentoMessenger;


        public FluxoBusiness(ILuzService luzService, RawDataRepository rawData, IMapper mapper, FluxosRepository fluxos, ISender rabbitSender, DadosCaracteristicosRepository dadosCaracteristicos, RastreamentoMessenger rastreamentoMessenger)
        {
            _luzService = luzService;
            _rawData = rawData;
            _mapper = mapper;
            _fluxos = fluxos;
            _rabbitSender = rabbitSender;
            //_assinatura = assinaturaRepository;
            //_config = config;
            //_hangfire = hangfire;
            _dadosCaracteristicos = dadosCaracteristicos;
            _rastreamentoMessenger = rastreamentoMessenger;

        }

        public List<Fluxos> RelatorioFluxo(string user, DateTime data, int tentativa, Guid? idEventoCadastro = null)
        {
            var fluxoHandler = new FluxoHandler(_rastreamentoMessenger);
            var tentativaHandler = new ValidadorTentativaHandler<List<Fluxos>>(_config, _hangfire, _rastreamentoMessenger, _mapper);
            var rawDataHandler = new RawDataHandler<List<Fluxos>>(_rawData, _mapper);
            //var fluxoPersistence = new FluxoPersistenceHandler(_fluxos, _mapper);

            fluxoHandler.SetNext(tentativaHandler)
                    .SetNext(rawDataHandler);
            //.SetNext(fluxoPersistence);

            //tentativaHandler.RetryUrl = "v2/Fluxo/relatorio-dia/";

            if(idEventoCadastro== new Guid())
            {
                idEventoCadastro = Guid.NewGuid();
            }

            var mensagemEvento = _rastreamentoMessenger.MontarObjetoEventoFilho((Guid)idEventoCadastro, TipoRequisicaoEnum.FLUXO, null, "v2/Fluxo/relatorio-dia/", StatusProcessamentoEnum.INICIADO, new JObject(), user);
            _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);

            List<Fluxos> relatorioCompleto = new List<Fluxos>();
            //List<Fluxos> relatorio = null;
            HttpResponseHeaders headers = null;
            string hasNext = "";

            //UNITARIO1
            var papeisAssinados = _luzService.PapeisAssinados().Result;

            foreach (string p in papeisAssinados)
            {
                List<Fluxos> relatorio = null;

                do
                {
                    (relatorio, headers) = _luzService.FluxoPapel(data, p, hasNext).Result;

                    //tentativaHandler.Tentativa = tentativa;
                    fluxoHandler.Handle(relatorio, EProcessStep.MDP);

                    relatorioCompleto.AddRange(relatorio);

                    var hash = Utils.GenerateHash(p);
                    var ultimaAtt = _fluxos.GetUltimaAtualizacao(hash);
                    var fluxosPapel = new List<FluxosDAO>();

                    if (DateTime.Compare(data.Date, ultimaAtt.Date) >= 0)
                    {
                        //Atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                        (fluxosPapel, _) = NewFlowRoutine(relatorio, p, hash, data, ultimaAtt, 10, 1);
                    }

                    //hasNext
                    IEnumerable<string> values;
                    if (headers.TryGetValues("next", out values))
                    {                       
                        var uri = new UriBuilder(values.FirstOrDefault());
                        hasNext = uri.Query;
                    }
                    else
                    {
                        hasNext = "";
                    }
                    

                } while (hasNext != "");              

            }
            _rastreamentoMessenger.FinalizarEventoEmAndamento();

            return relatorioCompleto;
        }

        private (List<FluxosDAO>, int) NewFlowRoutine(List<Fluxos> relatorio, string papel, byte[] hash, DateTime data, DateTime ultimaAtt, int pageSize, int page)
        {
            int totalPapeis = 0;
            var fluxosPapel = new List<FluxosDAO>();

            //_logger.LogInformation("Buscando relatório de fluxos de hoje para o papel: " + papel);
            if (relatorio.Count().Equals(0))
            {
                //_logger.LogInformation("Novos fluxos para o dia de hoje não foram encontrados");
                totalPapeis = _fluxos.CountByHash(hash, ultimaAtt);
                if (totalPapeis.Equals(0))
                {
                    return (fluxosPapel, totalPapeis);
                }
                fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, ultimaAtt, pageSize, page - 1));
            }
            else
            {
                //_logger.LogInformation("Atualizando dados de fluxo nas bases de dados");
                try
                {
                    var DAORelatorio = _mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio);
                    var relatorioDataVerificada = VerificaDataBase(relatorio, hash);
                    _rawData.BulkInsert(DAORelatorio);
                    _fluxos.BulkInsert(_mapper.Map<List<FluxosDAO>>(relatorioDataVerificada));
                    data = DateTime.Now;
                    totalPapeis = _fluxos.CountByHash(hash, data);
                    fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, data, pageSize, page - 1));

                    IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioDataVerificada), StatusMensagemEnum.PERSISTIDO_BPO, "");
                    _rastreamentoMessenger.SendTrackingMessage(papeis);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return (fluxosPapel, totalPapeis);

        }
        private List<Fluxos> VerificaDataBase(List<Fluxos> relatorio, byte[] codSnaHash)
        {
            var dtInitRent = _dadosCaracteristicos.DataInicioRentabilidade(codSnaHash);
            relatorio.Reverse();

            foreach (Fluxos fluxo in relatorio)
            {
                if (fluxo.DataBase <= dtInitRent)
                {
                    relatorio.Remove(fluxo);
                    break;
                }
            }
            return relatorio;
        }

    }
}