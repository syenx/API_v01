using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using EDM.Infohub.BPO.Models.SQS;

namespace EDM.Infohub.BPO.Controllers.v1
{
    [ApiVersion("1", Deprecated = false)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class PuDeEventosController : Controller
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<PuDeEventosController> _logger;
        private readonly RawDataRepository _rawData;
        private readonly IMapper _mapper;
        private PrecosRepository _precos;
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IConfiguration _config;
        private HangfireService _hangfire;
        private PuDeEventosRepository _puDeEventosRepository;
        private IFireForgetRepositoryHandler _fireforget;
        private RastreamentoMessenger _rastreamentoMessenger;
        private FluxosRepository _fluxos;

        public PuDeEventosController(ILogger<PuDeEventosController> logger, IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire, PuDeEventosRepository puDeEventosRepository, RastreamentoMessenger rastreamentoMessenger, IFireForgetRepositoryHandler fireForget, FluxosRepository fluxosRepository)
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
            _puDeEventosRepository = puDeEventosRepository;
            _fireforget = fireForget;
            _fluxos = fluxosRepository;
        }

        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioDia([FromHeader] string user, [FromQuery] DateTime data, [FromQuery] int tentativa = 0)
        {
            data = Utils.FilterDateParam(data);

            var puDeEventosBussiness = new PuDeEventoBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _precos, _rabbitSender, _hangfire, _puDeEventosRepository, _rastreamentoMessenger, _fireforget);

            List<PuDeEventos> relatorio = null;
            try
            {
                relatorio = puDeEventosBussiness.RelatorioPuDeEventos(data, tentativa, user).Result;

            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v1/PuDeEventos/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v1/PuDeEventos/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, "/v1/PuDeEventos/relatorio-dia", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                _logger.LogError(e, e.Message);
                return Problem(detail: e.Message, statusCode: 500);
            }

            return Ok(new Items<PuDeEventos>() { items = relatorio });
        }

        [HttpGet]
        [Route("{papel}")]
        public IActionResult RelatorioPapelPuDeEventos([FromHeader] string user, [FromQuery] DateTime data, [FromRoute] string papel, [FromQuery] int tentativa = 0)
        {
            data = Utils.FilterDateParam(data);

            var puDeEventosBussiness = new PuDeEventoBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _precos, _rabbitSender, _hangfire, _puDeEventosRepository, _rastreamentoMessenger, _fireforget);

            List<PuDeEventos> relatorio = null;
            try
            {
                relatorio = puDeEventosBussiness.RelatorioPuDeEventosPapel(data, papel, tentativa, user).Result;

            }
            catch (IOException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/PuDeEventos/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                var jsonError = JObject.FromObject(new { message = "", erro = e.Message });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/PuDeEventos/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
                if(e.Message.Contains("O relatório da Luz está vazio"))
                {
                    var hash = Utils.GenerateHash(papel);
                    errorMessage = _fluxos.GetByHash(hash,null, int.MaxValue, 0, data).Count().Equals(0)? "O papel não tem nenhum evento registrado na data requisitada" : "Os dados do evento não foram recebidos";
                    
                }
                var jsonError = JObject.FromObject(new { message = "", erro = errorMessage });
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), DateTime.Now, $"/v1/PuDeEventos/{papel}", StatusProcessamentoEnum.ERRO, jsonError, "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
                _rastreamentoMessenger.FinalizarEventoEmAndamento();
                _logger.LogError(e, errorMessage);
                return Problem(detail: errorMessage, statusCode: 500);
            }

            return Ok(new Items<PuDeEventos>() { items = relatorio });
        }
    }
}
