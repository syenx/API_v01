using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class InfohubLuzController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<InfohubLuzController> _logger;
        private AssinaturaRepository _assinatura;

        public InfohubLuzController(ILogger<InfohubLuzController> logger, ILuzService luzService, AssinaturaRepository assinatura)
        {
            _luzService = luzService;
            _logger = logger;
            _assinatura = assinatura;
        }

        [HttpGet]
        [Route("autenticacao")]
        public async System.Threading.Tasks.Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Getting Autentication");

            var autenticacao = await _luzService.AutenticationAsync();
            return Ok(JsonConvert.SerializeObject(autenticacao));

        }
    }
}
