using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.DataAccess.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace EDM.Infohub.BPO.Controllers.V2
{
    [ApiVersion("2", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class DashboardController : ControllerBase
    {
        private ILogger<DashboardController> _logger;
        private PrecosRepository _precos;
        private IConfiguration _config;
        private AssinaturaRepository _assinatura;
        private RastreamentoPapelRepository _rastreamento;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration config, PrecosRepository preco, AssinaturaRepository assinaturaRepository, RastreamentoPapelRepository rastreamentoPapelRepository)
        {
            _logger = logger;
            _precos = preco;
            _config = config;
            _assinatura = assinaturaRepository;
            _rastreamento = rastreamentoPapelRepository;
        }

        [HttpGet]
        public IActionResult GetDashboardData([FromQuery] DateTime data, [FromQuery] string tipoRequisicao = "")
        {
            data = Utils.FilterDateParam(data);
            try
            {
                var (impactaPreco, impactaEvento, impactaCadastro) = ParseTipoRequisicao(tipoRequisicao);
                var totalMarcados = _assinatura.CountAssinados("", impactaPreco ?? null, impactaCadastro ?? null, impactaEvento ?? null, null, null);
                var impactadosHoje = _rastreamento.CountImpactosdoDia(data, tipoRequisicao);

                return Ok(new { qtdMarcados = totalMarcados, qtdImpactados = impactadosHoje });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem();
            }
        }

        private (bool?, bool?, bool?) ParseTipoRequisicao(string tipoRequisicao)
        {
            if(tipoRequisicao == "PRECO")
            {
                return (true, null, null);
            }
            if (tipoRequisicao == "EVENTO")
            {
                return (null, true, null);
            }
            if (tipoRequisicao == "CADASTRO")
            {
                return (null, null, true);
            }
            return (null, null, null);
        }

    }
}
