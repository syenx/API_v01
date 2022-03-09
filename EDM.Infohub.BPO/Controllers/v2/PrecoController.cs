using Amazon.SecretsManager;
using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDM.Infohub.BPO.Controllers.v2
{
    [ApiVersion("2", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class PrecoController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<PrecoController> _logger;
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

        public PrecoController(ILogger<PrecoController> logger, IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire, IAmazonSecretsManager secret, RastreamentoMessenger rastreamentoMessenger, DadosCaracteristicosRepository cadastro)
        {
            _luzService = luzService;
            _logger = logger;
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

        [HttpGet]
        public IActionResult RelatorioDiaFront([FromQuery] DateTime dataEvento, [FromQuery] DateTime dataCriacaoInicio, [FromQuery] DateTime dataCriacaoFim, [FromQuery] string search = "", [FromQuery] string codigoSNA = "", [FromQuery] string tipo = null, [FromQuery] string horaCriacaoFim = "2359", [FromQuery] string horaCriacaoInicio = "0000", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            dataEvento = Utils.FilterDateParam(dataEvento);
            TimeSpan timeCriacaoInicio = Utils.FilterTimeParam(horaCriacaoInicio);
            dataCriacaoInicio = Utils.FilterDateParam(dataCriacaoInicio);
            TimeSpan timeCriacaoFim = Utils.FilterTimeParam(horaCriacaoFim);
            dataCriacaoFim = Utils.FilterDateParam(dataCriacaoFim);
            _logger.LogInformation("Buscando preços do dia para a tabela do portal");

            var totalPapeis = _precos.CountFilteredPrecosDatas(dataEvento, search.Length.Equals(0) ? codigoSNA : search, tipo, dataCriacaoInicio, timeCriacaoInicio, dataCriacaoFim ,timeCriacaoFim);
            var relatorio = _precos.GetFilteredPrecosDatas(dataEvento, search.Length.Equals(0) ? codigoSNA : search, tipo, dataCriacaoInicio, timeCriacaoInicio, dataCriacaoFim, timeCriacaoFim, pageSize, page - 1);

            var relatorioFormatado = new ListResponse<DadosPrecoLuz>()
            {
                Items = _mapper.Map<List<DadosPrecoDAO>, List<DadosPrecoLuz>>(relatorio.ToList()),
                HasNext = !(totalPapeis - (pageSize * (page - 1)) <= pageSize)
            };

            return Ok(relatorioFormatado);
        }

        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioDia([FromHeader] string user, [FromQuery] DateTime data, [FromQuery] int tentativa = 0)
        {

            data = Utils.FilterDateParam(data);

            var precosBussiness = new PrecoBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _precos, _rabbitSender, _hangfire, _rastreamentoMessenger, _secret, _cadastro);

            List<DadosPrecoLuz> relatorio = null;
            try
            {
                relatorio = precosBussiness.RelatorioPreco(data, tentativa, user);
            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Preco/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Preco/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Preco/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                _logger.LogError(e, e.Message);
                return Problem(detail: e.Message, statusCode: 500);
            }

            return Ok(new Items<DadosPrecoLuz>() { items = relatorio });
        }


        [HttpGet]
        [Route("{papel}")]
        public IActionResult RelatorioPapelPrecos([FromRoute] string papel, [FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, [FromQuery] string timeStartSTR = "0000", [FromQuery] string timeEndSTR = "2359", [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var hashPapel = Utils.GenerateHash(papel);
            dateStart = Utils.FilterDateParam(dateStart);
            dateEnd = Utils.FilterDateParam(dateEnd);
            TimeSpan timeStart = Utils.FilterTimeParam(timeStartSTR);
            TimeSpan timeEnd = Utils.FilterTimeParam(timeEndSTR);

            if (String.IsNullOrEmpty(papel))
            {
                _logger.LogError("String Papel não foi preenchida");
                return BadRequest(new { error = "Parametro papel está vazio" });
            }
            _logger.LogInformation("Buscando relatório de preços para o papel: " + papel);

            var totalPapeis = _precos.CountByTimeInterval(hashPapel, dateStart, timeStart, dateEnd, timeEnd);
            var histprecoPapel = _precos.GetByTimeInterval(hashPapel, pageSize, page - 1, dateStart, timeStart, dateEnd, timeEnd);

            if (histprecoPapel.Count().Equals(0)) //String.IsNullOrEmpty(precoPapel.cd_sna))
            {
                var message = "Nenhum registro de preço encontrado para o papel " + papel;
                return NotFound(new { message = message });
            }

            var histPrecoFormatado = new ListResponse<DadosPrecoLuz>()
            {
                Items = _mapper.Map<List<DadosPrecoDAO>, List<DadosPrecoLuz>>(histprecoPapel.ToList()),
                HasNext = !(totalPapeis - (pageSize * (page - 1)) <= pageSize)
            };

            return Ok(histPrecoFormatado);

        }

        [HttpGet]
        [Route("csv")]
        public FileContentResult RelatorioPapelPrecosCsv([FromQuery] DateTime dataEvento, [FromQuery] DateTime dataCriacaoInicio, [FromQuery] DateTime dataCriacaoFim, [FromQuery] string codigoSNA = "", [FromQuery] string tipo = null, [FromQuery] string horaCriacaoFim = "2359", [FromQuery] string horaCriacaoInicio = "0000")
        {
            dataEvento = Utils.FilterDateParam(dataEvento);
            TimeSpan timeCriacaoInicio = Utils.FilterTimeParam(horaCriacaoInicio);
            dataCriacaoInicio = Utils.FilterDateParam(dataCriacaoInicio);
            TimeSpan timeCriacaoFim = Utils.FilterTimeParam(horaCriacaoFim);
            dataCriacaoFim = Utils.FilterDateParam(dataCriacaoFim);
            _logger.LogInformation("Buscando preços do dia para a tabela do portal");
            _logger.LogInformation("Convertendo relatorio de preços em arquivo csv...");
            _logger.LogInformation("Buscando relatório de preços do dia");

            var relatorio = _precos.GetFilteredPrecosDatas(dataEvento, codigoSNA, tipo, dataCriacaoInicio, timeCriacaoInicio, dataCriacaoFim, timeCriacaoFim);

            var relatorioFormatado = new ListResponse<DadosPrecoLuz>()
            {
                Items = _mapper.Map<List<DadosPrecoDAO>, List<DadosPrecoLuz>>(relatorio.ToList()),
            };

            _logger.LogInformation("Convertendo assinaturas em arquivo csv...");

            var csv = relatorioFormatado.Items.ToCsv();
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "PrecosLuz.csv");
        }

        [HttpGet]
        [Route("csvHist")]
        public FileContentResult HistoricoPapelPrecosCsv([FromQuery] string papel, [FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, [FromQuery] string timeStartSTR = "0000", [FromQuery] string timeEndSTR = "2359")
        {
            var hashPapel = Utils.GenerateHash(papel);
            dateStart = Utils.FilterDateParam(dateStart);
            dateEnd = Utils.FilterDateParam(dateEnd);
            TimeSpan timeStart = Utils.FilterTimeParam(timeStartSTR);
            TimeSpan timeEnd = Utils.FilterTimeParam(timeEndSTR);

            _logger.LogInformation("Buscando histórico de preços do papel " + papel);

            var totalPapeis = _precos.CountByTimeInterval(hashPapel, dateStart, timeStart, dateEnd, timeEnd);
            var histprecoPapel = _precos.GetByTimeInterval(hashPapel, totalPapeis, 0, dateStart, timeStart, dateEnd, timeEnd);

            var relatorioFormatado = new ListResponse<DadosPrecoLuz>()
            {
                Items = _mapper.Map<List<DadosPrecoDAO>, List<DadosPrecoLuz>>(histprecoPapel.ToList()),
            };

            _logger.LogInformation("Convertendo o historico de preços em arquivo csv...");

            var csv = relatorioFormatado.Items.ToCsv();
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "HistPreco" + papel + ".csv");
        }

        [HttpGet]
        [Route("tipo")]
        public IActionResult TiposPapel()
        {
            _logger.LogInformation("Retornando tipos de papel da tabela de preços");

            var tipos = _precos.GetTypes();
            return Ok(tipos);

        }
        [HttpGet]
        [Route("ultimaAtt")]
        public IActionResult GetDataUltimoPreco([FromQuery] string papel)
        {
            var hash = Utils.GenerateHash(papel);
            _logger.LogInformation("Retornando data do ultimo preço recebido");

            return Ok(_precos.GetUltimaAtualizacaoPapel(hash));

        }
    }
}