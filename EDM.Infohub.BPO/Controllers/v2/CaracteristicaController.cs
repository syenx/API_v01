using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using EDM.Infohub.BPO.SQS;
using EDM.Infohub.BPO.SQS.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V2
{
    [ApiVersion("2", Deprecated = false)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class CaracteristicaController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<CaracteristicaController> _logger;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private AssinaturaRepository _assinatura;
        private AssinaturaLogRepository _asslogRepository;
        private ISender _rabbitSender;
        private IFireForgetRepositoryHandler _fireForget;
        private HangfireService _hangfire;
        private IConfiguration _config;
        private RastreamentoMessenger _rastreamentoMessenger;

        public CaracteristicaController(ILogger<CaracteristicaController> logger, ILuzService luzService, RawDataRepository rawData, IMapper mapper, DadosCaracteristicosRepository dadosCaracteristicos, AssinaturaRepository assinaturaRepository, ISender rabbitSender, IFireForgetRepositoryHandler fireForget, HangfireService hangfire, AssinaturaLogRepository logRepository, IConfiguration configuration, RastreamentoMessenger rastreamentoMessenger)
        {
            _luzService = luzService;
            _logger = logger;
            _rawData = rawData;
            _mapper = mapper;
            _dadosCaracteristicos = dadosCaracteristicos;
            _assinatura = assinaturaRepository;
            _asslogRepository = logRepository;
            _rabbitSender = rabbitSender;
            _fireForget = fireForget;
            _hangfire = hangfire;
            _config = configuration;
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioCaracteristicaDia([FromHeader] string user, [FromQuery] DateTime data, [FromQuery] int tentativa = 0)
        {
           
            data = Utils.FilterDateParam(data);
            _logger.LogInformation($"Buscando relatorio de caracteristica do dia: {data.ToShortDateString()}");

            var caracteristicaBusiness = new CaracteristicaBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _dadosCaracteristicos, _rabbitSender, _hangfire, _rastreamentoMessenger);

            try
            {
                var relatorio = caracteristicaBusiness.RelatorioCaracteristica(data, tentativa, user);

                //var idEventoCadastro = _rastreamentoMessenger.EventoEmAndamento.IdRequisicao;
                //_rastreamentoMessenger.FinalizarEventoEmAndamento();

                //fireforget
                //_fireForget.ExecuteFluxos(async fluxos =>
                //{
                //    // Will receive its own scoped repository on the executing task
                //    fluxos.RelatorioFluxoDia(user, idEventoCadastro, data);
                //});
                
                return Ok(relatorio);
            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Caracteristica/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Caracteristica/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v2/Caracteristica/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
        }

        [HttpGet]
        [Route("{papel}")]
        public IActionResult RelatorioPapelDadosCaracteristicos([FromRoute] string papel)
        {
            if (String.IsNullOrEmpty(papel))
            {
                _logger.LogError("String Papel não foi preenchida");
                return BadRequest(new { error = "Parametro papel está vazio" });
            }
            _logger.LogInformation("Buscando relatório de características para o papel: " + papel);

            var caracteristicaPapel = _dadosCaracteristicos.GetByHash(Utils.GenerateHash(papel));

            if (String.IsNullOrEmpty(caracteristicaPapel.cd_sna))
            {
                var message = "Nenhum registro de cadastro encontrado para o papel " + papel;
                return NotFound(new { message = message });
            }

            return Ok(_mapper.Map<DadosCaracteristicos>(caracteristicaPapel));

        }

        [HttpGet]
        [Route("csv")]
        public FileContentResult RelatorioDadosCaracteristicosCsv()
        {
            _logger.LogInformation("Convertendo relatorio de dados caracteristicos em arquivo csv...");

            var pageSize = 1000;
            var rowsRead = 0;
            var page = 1;
            var sizeRelatorio = _dadosCaracteristicos.CountDadosCaracteristicosAssinados();
            var csv = "";

            while (rowsRead < sizeRelatorio)
            {
                var relatorio = _dadosCaracteristicos.DadosCaracteristicosAssinados(pageSize, page - 1);
                var relatorioFormatado = new ListResponse<DadosCaracteristicos>()
                {
                    Items = _mapper.Map<List<DadosCaracteristicosDAO>, List<DadosCaracteristicos>>(relatorio.ToList()),
                };
                var formatCSV = relatorioFormatado.Items.ToCsv();
                if (page > 1)
                {
                    var lines = Regex.Split(formatCSV, "\r\n|\r|\n").Skip(1);
                    formatCSV = string.Join(Environment.NewLine, lines.ToArray());
                }
                csv = csv + formatCSV;

                page++;
                rowsRead = rowsRead + pageSize;

            }

            var csvByte = Encoding.UTF8.GetBytes(csv);
            return File(Encoding.UTF8.GetPreamble().Concat(csvByte).ToArray(), "text/csv", "CadastrosLuz.csv");
        }

        [HttpGet]
        [Route("relatorio-dia/{papel}")]
        public IActionResult RelatorioCaracteristicaDia([FromHeader] string user, [FromQuery] DateTime data, [FromRoute] string papel, [FromQuery] int tentativa = 0)
        {
            data = Utils.FilterDateParam(data);
            _logger.LogInformation($"Buscando relatorio de caracteristica do dia: {data.ToShortDateString()}");

            var caracteristicaBusiness = new CaracteristicaBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _dadosCaracteristicos, _rabbitSender, _hangfire, _rastreamentoMessenger);

            try
            {
                var (relatorio, messages) = caracteristicaBusiness.RelatorioCaracteristicaPapel(data, papel, tentativa, user);

                //fireforget
                //_fireForget.ExecuteFluxos(async fluxos =>
                //{
                //    // Will receive its own scoped repository on the executing task
                //    await fluxos.RelatorioFluxoDiaUnitario(data, papel);
                //});

                return Ok(messages);
            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"v2/Caracteristica/relatorio-dia/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"v2/Caracteristica/relatorio-dia/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"v2/Caracteristica/relatorio-dia/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
        }

        [HttpPost]
        [Route("relatorio-dia/lote")]
        public IActionResult RelatorioLoteCaracteristicaDia([FromHeader] string user, [FromQuery] DateTime data, IFormFile file)
        {
            data = Utils.FilterDateParam(data);
            var papeis = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                _logger.LogInformation("Arquivo csv recebido.");
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        var result = new StringBuilder();
                        result.AppendLine(reader.ReadLine());
                        var items = result.ToString().Split('\n');
                        if (!items[0].ToLower().Equals("papel") && !String.IsNullOrWhiteSpace(items[0]))
                            papeis.Add(items[0].Trim().ToUpper());
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Erro ao tentar ler o arquivo csv: " + e.Message);
                        break;
                    }
                }
            }

            if (papeis.Count.Equals(0))
            {
                var message = "Erro ao tentar ler o arquivo csv: Formato dos dados não é valido";
                _logger.LogError(message);
                return Problem(statusCode: 500, title: message);
            }

            try
            {
                foreach(string papel in papeis)
                {
                    _fireForget.ExecuteCadastro(async caracteristica =>
                    {
                        caracteristica.RelatorioCaracteristicaDia(user, data, papel);
                    });
                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.StackTrace, instance: e.Message, statusCode: 500, title: "Erro na atualização de cadastro em lote");
            }

        }
    }
}
