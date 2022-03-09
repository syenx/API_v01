using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;

namespace EDM.Infohub.BPO.Controllers.V2
{
    [ApiVersion("2")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class FluxoController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<FluxoController> _logger;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private FluxosRepository _fluxos;
        private ISender _rabbitSender;
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private RastreamentoMessenger _rastreamentoMessenger;

        public FluxoController(ILogger<FluxoController> logger, ILuzService luzService, RawDataRepository rawData, IMapper mapper, FluxosRepository fluxos, ISender rabbitSender, DadosCaracteristicosRepository dadosCaracteristicos, RastreamentoMessenger rastreamentoMessenger)
        {
            _luzService = luzService;
            _logger = logger;
            _rawData = rawData;
            _mapper = mapper;
            _fluxos = fluxos;
            _rabbitSender = rabbitSender;
            _dadosCaracteristicos = dadosCaracteristicos;
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioFluxoDia([FromHeader] string user, [FromQuery] Guid idRequisicaoEvento, [FromQuery] DateTime data, int tentativa = 0)
        {
            data = Utils.FilterDateParam(data);

            _logger.LogInformation("Buscando relatorio de fluxo do dia");

            var fluxoBusiness = new FluxoBusiness(_luzService, _rawData, _mapper, _fluxos, _rabbitSender, _dadosCaracteristicos, _rastreamentoMessenger);

            try
            {
                _ = fluxoBusiness.RelatorioFluxo(user, data, tentativa, idRequisicaoEvento);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(new { mensagem = "Os Fluxos foram atualizados" });
        }

        [HttpGet]
        [Route("relatorio-dia/{papel}")]
        public IActionResult RelatorioFluxoDiaUnitario([FromQuery] DateTime data, [FromRoute] string papel)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                data = DateTime.Now;
            }
            _logger.LogInformation($"Buscando relatorio de fluxo do dia para {papel}");
            //Console.WriteLine("Buscando relatorio de fluxo do dia");
            //var papeisAssinados = await _luzService.PapeisAssinados();

            List<Fluxos> relatorio = new List<Fluxos>();
            try
            {
                string hasNext = "";               

                do
                {
                    HttpResponseHeaders headers = null;
                    List<Fluxos> pagina = null;
                    (pagina, headers) = _luzService.FluxoPapel(data, papel, hasNext).Result;

                    //tentativaHandler.Tentativa = tentativa;
                    //fluxoHandler.Handle(relatorio, EProcessStep.MDP);

                    relatorio.AddRange(pagina);

                    //hasNext
                    var teste = headers.GetValues("next").ToList();
                    var uri = new UriBuilder(teste.FirstOrDefault());
                    hasNext = uri.Query;

                } while (hasNext != "");



                if (relatorio.Count.Equals(0))
                {
                    _logger.LogInformation("Nao foi encontrado relatorio de fluxo do dia " + data);
                }
                _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio));

                var hash = Utils.GenerateHash(papel);
                var ultimaAtt = _fluxos.GetUltimaAtualizacao(hash);
                var fluxosPapel = new List<FluxosDAO>();

                //requisição de um fluxo novo (EM RELAÇAO AOS FLUXOS EXISTENTES NA TABELA)
                if (DateTime.Compare(data.Date, ultimaAtt.Date) >= 0)
                {

                    //Atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                    (fluxosPapel, _) = NewFlowRoutine(relatorio, papel, hash, data, ultimaAtt, 10, 1);

                    //_fluxos.BulkInsert(_mapper.Map<List<FluxosDAO>>(relatorio));
                    //var relatorio = await _luzService.RelatorioFluxo(data);
                }

                relatorio = VerificaDataBase(relatorio, hash);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(relatorio);
        }

        [HttpGet]
        [Route("{papel}")]
        public IActionResult RelatorioPapelFluxos([FromHeader] string user, [FromRoute] string papel, [FromQuery] DateTime data, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                data = DateTime.Now;
            }
            if (String.IsNullOrEmpty(papel) || DateTime.Compare(data.Date, DateTime.Now.Date) > 0)
            {
                _logger.LogError("Erro nos parametros de entrada");
                return BadRequest(new { error = "Dados de entrada não coerentes" });
            }
            _logger.LogInformation("Buscando relatório de fluxos para o papel: " + papel);

            var hash = Utils.GenerateHash(papel);

            string hasNext = "";

            List<Fluxos> relatorio = new List<Fluxos>();

            do
            {
                HttpResponseHeaders headers = null;
                List<Fluxos> pagina = null;
                (pagina, headers) = _luzService.FluxoPapel(data, papel, hasNext).Result;

                //tentativaHandler.Tentativa = tentativa;
                //fluxoHandler.Handle(relatorio, EProcessStep.MDP);

                relatorio.AddRange(pagina);

                //hasNext
                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;

            } while (hasNext != "");

            var ultimaAtt = _fluxos.GetUltimaAtualizacao(hash);
            int totalPapeis = 0;
            var fluxosPapel = new List<FluxosDAO>();


            //requisição de um fluxo novo (EM RELAÇAO AOS FLUXOS EXISTENTES NA TABELA)
            if (DateTime.Compare(data.Date, ultimaAtt.Date) >= 0)
            {
                //se não existe fluxo para esse papel na base e a data requisitada é anterior ao dia atual
                if (ultimaAtt.Equals(new DateTime(0001, 01, 01)) && DateTime.Compare(data.Date, DateTime.Now.Date) < 0)
                {
                    //Não atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                    (fluxosPapel, totalPapeis) = OldFlowRoutine(relatorio, papel, hash, data, ultimaAtt, pageSize, page);
                }
                else
                {
                    //Atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                    (fluxosPapel, totalPapeis) = NewFlowRoutine(relatorio, papel, hash, data, ultimaAtt, pageSize, page);
                }

            }

            //requisição de um fluxo antigo (EM RELACAO AOS FLUXOS EXISTENTES NA TABELA DE FLUXO)
            if (DateTime.Compare(data.Date, ultimaAtt.Date) < 0)
            {
                //Não atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                (fluxosPapel, totalPapeis) = OldFlowRoutine(relatorio, papel, hash, data, ultimaAtt, pageSize, page);
            }

            if (totalPapeis.Equals(0))
            {
                var message = "Nao foi encontrado relatorio de fluxo do papel " + papel;
                _logger.LogInformation(message);
                return NotFound(new { message = message });
            }

            var relatorioFormatado = new ListResponse<Fluxos>()
            {
                Items = _mapper.Map<List<Fluxos>>(fluxosPapel),
                HasNext = !(totalPapeis - (pageSize * (page - 1)) <= pageSize)
            };

            return Ok(relatorioFormatado);
        }

        private (List<FluxosDAO>, int) NewFlowRoutine(List<Fluxos> relatorio, string papel, byte[] hash, DateTime data, DateTime ultimaAtt, int pageSize, int page)
        {
            int totalPapeis = 0;
            var fluxosPapel = new List<FluxosDAO>();

            _logger.LogInformation("Buscando relatório de fluxos de hoje para o papel: " + papel);
            if (relatorio.Count().Equals(0))
            {
                _logger.LogInformation("Novos fluxos para o dia de hoje não foram encontrados");
                totalPapeis = _fluxos.CountByHash(hash, ultimaAtt);
                if (totalPapeis.Equals(0))
                {
                    return (fluxosPapel, totalPapeis);
                }
                fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, ultimaAtt, pageSize, page - 1));
            }
            else
            {
                _logger.LogInformation("Atualizando dados de fluxo nas bases de dados");
                try
                {
                    var DAORelatorio = _mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio);
                    var relatorioDataVerificada = VerificaDataBase(relatorio, hash);
                    _rawData.BulkInsert(DAORelatorio);
                    _fluxos.BulkInsert(_mapper.Map<List<FluxosDAO>>(relatorioDataVerificada));
                    data = DateTime.Now;
                    totalPapeis = _fluxos.CountByHash(hash, data);
                    fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, data, pageSize, page - 1));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return (fluxosPapel, totalPapeis);

        }
        private (List<FluxosDAO>, int) OldFlowRoutine(List<Fluxos> relatorio, string papel, byte[] hash, DateTime data, DateTime ultimaAtt, int pageSize, int page)
        {
            int totalPapeis = 0;
            var fluxosPapel = new List<FluxosDAO>();

            _logger.LogInformation("Buscando relatório de fluxos antigo para o papel: " + papel);
            if (relatorio.Count().Equals(0))
            {
                return (fluxosPapel, totalPapeis);
            }
            else
            {
                _logger.LogInformation("Atualizando dados de fluxo antigo na base de dados");
                try
                {
                    var DAORelatorio = _mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio);
                    _rawData.BulkInsert(DAORelatorio);
                    totalPapeis = relatorio.Count();
                    var oldFlow = _rawData.GetOldFlow(DAORelatorio, hash, pageSize, page - 1);
                    relatorio.Clear();
                    foreach (var flow in oldFlow)
                    {
                        relatorio.Add(JsonConvert.DeserializeObject<Fluxos>(flow.tx_json_str));
                    }
                    fluxosPapel = _mapper.Map<List<FluxosDAO>>(relatorio);
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

        [HttpGet]
        [Route("csv")]
        public FileContentResult RelatorioFluxosCsv()
        {
            _logger.LogInformation("Convertendo fluxos em arquivo csv...");

            var relatorio = _fluxos.FluxosAssinados();

            var relatorioFormatado = new ListResponse<Fluxos>()
            {
                Items = _mapper.Map<List<FluxosDAO>, List<Fluxos>>(relatorio.ToList()),
            };

            var csv = relatorioFormatado.Items.ToCsv();
            var csvByte = Encoding.UTF8.GetBytes(csv);
            return File(Encoding.UTF8.GetPreamble().Concat(csvByte).ToArray(), "text/csv", "FluxosLuz.csv");
        }
    }
}
