using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
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
        public async Task<IActionResult> RelatorioFluxoDia([FromQuery] DateTime data)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                data = DateTime.Now;
            }
            _logger.LogInformation("Buscando relatorio de fluxo do dia");
            Console.WriteLine("Buscando relatorio de fluxo do dia");

            List<Fluxos> relatorio = null;

            (relatorio, _) = await _luzService.RelatorioFluxo(data, null);

            if (relatorio.Count.Equals(0))
            {
                _logger.LogInformation("Nao foi encontrado relatorio de fluxo do dia " + data);
                return NoContent();
            }

            try
            {
                _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio));
                //_fluxos.BulkInsert(_mapper.Map<List<FluxosDAO>>(relatorio));
                //_rabbitSender.SendLot<Fluxos>("FluxosQueue", relatorio, false);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(relatorio);
        }

        [HttpGet]
        [Route("{papel}")]
        public IActionResult RelatorioPapelFluxos([FromRoute] string papel, [FromQuery] DateTime data, [FromQuery] string dataRastreamento, [FromQuery] string usuario, [FromQuery] string idEvento = "", [FromQuery] bool homolog = false)
        {
            if (idEvento != "")
            {
                _rastreamentoMessenger.EventoEmAndamento = new RastreamentoEvento();
                _rastreamentoMessenger.EventoEmAndamento.IdRequisicao = Guid.Parse(idEvento);
                _rastreamentoMessenger.EventoEmAndamento.DataInicioEvento = DateTime.Parse(dataRastreamento);
                _rastreamentoMessenger.EventoEmAndamento.Usuario = usuario;
                _rastreamentoMessenger.EventoEmAndamento.TipoRequisicao = TipoLogEnum.FLUXO.ToString();
                _rastreamentoMessenger.EventoEmAndamento.StatusEvento = StatusProcessamentoEnum.PROCESSANDO.ToString();
            }

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
            
            if (idEvento != "")
            {
                IEnumerable<RastreamentoPapel> papeisRastream = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorio), StatusMensagemEnum.RECEBIDO_LUZ, "", homolog);
                _rastreamentoMessenger.SendTrackingMessage(papeisRastream);
            }

            var ultimaAtt = _fluxos.GetUltimaAtualizacao(hash);
            var fluxosPapel = new List<FluxosDAO>();

            //requisição de um fluxo novo (EM RELAÇAO AOS FLUXOS EXISTENTES NA TABELA)
            if (DateTime.Compare(data.Date, ultimaAtt.Date) >= 0)
            {
                //se não existe fluxo para esse papel na base e a data requisitada é anterior ao dia atual
                if (ultimaAtt.Equals(new DateTime(0001, 01, 01)) && DateTime.Compare(data.Date, DateTime.Now.Date) < 0)
                {
                    //Não atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                    fluxosPapel = OldFlowRoutine(relatorio, papel);
                }
                else
                {
                    //Atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                    fluxosPapel = NewFlowRoutine(relatorio, papel, hash, data, ultimaAtt);
                    if (idEvento != "")
                    {
                        IEnumerable<RastreamentoPapel> papeisRastream = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorio), StatusMensagemEnum.PERSISTIDO_BPO, "", homolog);
                        _rastreamentoMessenger.SendTrackingMessage(papeisRastream);
                    }
                }

            }

            //requisição de um fluxo antigo (EM RELACAO AOS FLUXOS EXISTENTES NA TABELA DE FLUXO)
            if (DateTime.Compare(data.Date, ultimaAtt.Date) < 0)
            {
                //Não atualiza a tabela de fluxos e retorna os dados paginados pro usuário
                fluxosPapel = OldFlowRoutine(relatorio, papel);
            }

            if (fluxosPapel.Count().Equals(0))
            {
                var message = "Nao foi encontrado relatorio de fluxo do papel " + papel;
                _logger.LogInformation(message);
                return NotFound(new { message = message });
            }
            if (idEvento != "")
            {
                IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorio), StatusMensagemEnum.ENVIADO_MDP, "", homolog);
                _rastreamentoMessenger.SendTrackingMessage(papeis);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
            }
            return Ok(_mapper.Map<List<Fluxos>>(fluxosPapel));
        }

        [HttpGet]
        [Route("csv")]
        public FileContentResult RelatorioPapelFluxosCsv([FromQuery] DateTime data)
        {
            _logger.LogInformation("Convertendo relatorio de fluxos em arquivo csv...");
            data = Utils.FilterDateParam(data);

            _logger.LogInformation("Buscando relatorio de fluxos do dia");

            List<Fluxos> relatorio = new List<Fluxos>();
            string hasNext = "";

            do
            {
                HttpResponseHeaders headers = null;
                List<Fluxos> pagina = null;

                (pagina, headers) = _luzService.RelatorioFluxo(data, hasNext).Result;

                relatorio.AddRange(pagina);
                //hasNext
                var teste = headers.GetValues("next").ToList();
                var uri = new UriBuilder(teste.FirstOrDefault());
                hasNext = uri.Query;

            } while (hasNext != "");
          
            var csv = relatorio.ToCsv();
            var csvByte = Encoding.UTF8.GetBytes(csv);
            return File(Encoding.UTF8.GetPreamble().Concat(csvByte).ToArray(), "text/csv", "FluxosLuz-" + data.ToString("dd-MM-yyyy") + ".csv");
        }

        private List<FluxosDAO> NewFlowRoutine(List<Fluxos> relatorio, string papel, byte[] hash, DateTime data, DateTime ultimaAtt)
        {
            var fluxosPapel = new List<FluxosDAO>();

            _logger.LogInformation("Buscando relatório de fluxos de hoje para o papel: " + papel);
            if (relatorio.Count().Equals(0))
            {
                _logger.LogInformation("Novos fluxos para o dia de hoje não foram encontrados");
                int totalPapeis = _fluxos.CountByHash(hash, ultimaAtt);
                if (totalPapeis.Equals(0))
                {
                    return fluxosPapel;
                }
                fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, ultimaAtt, totalPapeis, 0));
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
                    int totalPapeis = _fluxos.CountByHash(hash, data);
                    fluxosPapel = _mapper.Map<List<FluxosDAO>>(_fluxos.GetByHash(hash, data, totalPapeis, 0));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return fluxosPapel;
        }
        private List<FluxosDAO> OldFlowRoutine(List<Fluxos> relatorio, string papel)
        {
            var fluxosPapel = new List<FluxosDAO>();

            _logger.LogInformation("Buscando relatório de fluxos antigo para o papel: " + papel);
            if (relatorio.Count().Equals(0))
            {
                return fluxosPapel;
            }
            else
            {
                _logger.LogInformation("Atualizando dados de fluxo antigo na base de dados");
                try
                {
                    var DAORelatorio = _mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio);
                    _rawData.BulkInsert(DAORelatorio);
                    fluxosPapel = _mapper.Map<List<FluxosDAO>>(relatorio);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            return fluxosPapel;
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
