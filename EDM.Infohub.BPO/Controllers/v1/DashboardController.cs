using EDM.Infohub.BPO.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class DashboardController : ControllerBase
    {
        private ILogger<PrecoController> _logger;
        private PrecosRepository _precos;
        private IConfiguration _config;

        public DashboardController(ILogger<PrecoController> logger, IConfiguration config, PrecosRepository preco)
        {
            _logger = logger;
            _precos = preco;
            _config = config;

        }

        [HttpGet]
        public IActionResult PrecosDashboard()
        {
            var data = DateTime.Now;
            try
            {
                var ultimaAtualizacao = _precos.GetUltimaAtualizacao();
                var response = _precos.CountByData(ultimaAtualizacao);
                return Ok(new { papeisImpactados = response, ultimaAtualizacao = ultimaAtualizacao.ToString("dd/MM/yyyy HH:mm") });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem();
            }


        }

    }
}
