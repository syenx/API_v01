using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.Business;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class RecoveryController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<RecoveryController> _logger;
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

        public RecoveryController(ILogger<RecoveryController> logger, ILuzService luzService, RawDataRepository rawData, IMapper mapper, DadosCaracteristicosRepository dadosCaracteristicos, AssinaturaRepository assinaturaRepository, ISender rabbitSender, IFireForgetRepositoryHandler fireForget, HangfireService hangfire, AssinaturaLogRepository logRepository, IConfiguration configuration, RastreamentoMessenger rastreamentoMessenger)
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
        public IActionResult RelatorioCaracteristicaDia([FromHeader] string user, [FromQuery] DateTime data)
        {

            data = Utils.FilterDateParam(data);
            _logger.LogInformation($"Buscando relatorio de caracteristica do dia: {data.ToShortDateString()}");

            var caracteristicaBusiness = new CaracteristicaBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _dadosCaracteristicos, _rabbitSender, _hangfire, _rastreamentoMessenger);

            try
            {
                var relatorio = caracteristicaBusiness.RecoveryAll(data,user);

                //fireforget
                _fireForget.ExecuteFluxos(async fluxos =>
                {
                    // Will receive its own scoped repository on the executing task
                    fluxos.RelatorioFluxoDia(user, Guid.NewGuid(), data);
                });
                                                              
                return Ok(relatorio);
            }
            catch (IOException e)
            {
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.Message, statusCode: 500);
            }
        }

        [HttpGet]
        [Route("relatorio-dia/{papel}")]
        public IActionResult RelatorioCaracteristicaDia([FromHeader] string user, [FromQuery] DateTime data, [FromRoute] string papel)
        {
            data = Utils.FilterDateParam(data);
            _logger.LogInformation($"Buscando relatorio de caracteristica do dia: {data.ToShortDateString()}");

            var caracteristicaBusiness = new CaracteristicaBusiness(_config, _luzService, _rawData, _assinatura, _mapper, _dadosCaracteristicos, _rabbitSender, _hangfire, _rastreamentoMessenger);

            try
            {
                var relatorio = caracteristicaBusiness.RecoveryUnitity(data, papel, user);

                //fireforget
                _fireForget.ExecuteFluxos(async fluxos =>
                {
                    // Will receive its own scoped repository on the executing task
                    fluxos.RelatorioFluxoDiaUnitario(data, papel);
                });

                return Ok(relatorio);
            }
            catch (IOException e)
            {
                return NoContent();
            }
            catch (IndexOutOfRangeException e)
            {
                return Problem(detail: e.Message, statusCode: 500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.Message, statusCode: 500);
            }
        }

    }
}
