using Amazon.SecretsManager;
using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.Processamento;
using EDM.Infohub.BPO.Processamento.Impl.Preco;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = false)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class HistoricoController : Controller
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

        public HistoricoController(ILogger<PrecoController> logger, IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire, IAmazonSecretsManager secret, RastreamentoMessenger rastreamentoMessenger, DadosCaracteristicosRepository cadastro)
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
        [Route("{papel}")]
        public IActionResult RelatorioDia([FromHeader] string user, [FromRoute] string papel, [FromQuery] int tentativa = 0)
        {
            //data = Utils.FilterDateParam(data);
            _logger.LogInformation("Buscando relatório de preços históricos do dia");
            var precosBussiness = new PrecoBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _precos, _rabbitSender, _hangfire, _rastreamentoMessenger, _secret, _cadastro);

            List<DadosPrecoLuz> relatorio;
            try
            {
                Task.Run(async () =>
                {
                    relatorio = precosBussiness.HistoricoPapel(papel, tentativa, user);
                });
            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/Historico/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/Historico/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/Historico/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                _logger.LogError(e, e.Message);
                return Problem(detail: e.Message, statusCode: 500);
            }

            return Ok(new { message = $"Relatório de Histórico para {papel} requerido com sucesso" });
        }
    }
}
