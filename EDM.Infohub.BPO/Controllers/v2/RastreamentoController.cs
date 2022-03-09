using AutoMapper;
using EDM.Infohub.BPO.DataAccess.Impl;
using EDM.Infohub.BPO.Models.Response;
using EDM.Infohub.BPO.Models.SQS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using System.Text;
using EDM.Infohub.BPO.DataAccess;

namespace EDM.Infohub.BPO.Controllers.v2
{
    [ApiVersion("2", Deprecated = false)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class RastreamentoController : ControllerBase
    {
        private ILogger<RastreamentoController> _logger;
        private IMapper _mapper;
        private RastreamentoEventoRepository _rastreamentoEvento;
        private RastreamentoPapelRepository _rastreamentoPapel;
        private AssinaturaRepository _assinatura;

        public RastreamentoController(ILogger<RastreamentoController> logger, IMapper mapper, RastreamentoEventoRepository rastreamentoEventoRepository, RastreamentoPapelRepository rastreamentoPapelRepository, AssinaturaRepository assinaturaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rastreamentoEvento = rastreamentoEventoRepository;
            _rastreamentoPapel = rastreamentoPapelRepository;
            _assinatura = assinaturaRepository;
        }

        [HttpGet]
        [Route("evento")]
        public IActionResult GetEventos([FromQuery] DateTime dataInicioEvento, [FromQuery] string tipoRequisicao = "", [FromQuery] string statusEvento = "", [FromQuery] string usuario = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Exibindo eventos rastreados");
            dataInicioEvento = Utils.FilterDateParam(dataInicioEvento);
            var eventos = _rastreamentoEvento.GetAllEventos(dataInicioEvento, tipoRequisicao, statusEvento, usuario, pageSize, pageNumber - 1);
            var countEventos = _rastreamentoEvento.CountAllEventos(dataInicioEvento, tipoRequisicao, statusEvento, usuario);

            var formatado = new AssinaturaResponse<RastreamentoEventoPortal>()
            {
                Items = eventos.ToList(),
                HasNext = !(countEventos - (pageSize * (pageNumber - 1)) <= pageSize)
            };

            return Ok(formatado);
        }

        [HttpGet]
        [Route("evento/csv")]
        public FileContentResult GetEventosCsv([FromQuery] DateTime dataInicioEvento, [FromQuery] string tipoRequisicao = "", [FromQuery] string statusEvento = "", [FromQuery] string usuario = "")
        {
            _logger.LogInformation("Eventos rastreados em csv");
            dataInicioEvento = Utils.FilterDateParam(dataInicioEvento);
            var countEventos = _rastreamentoEvento.CountAllEventos(dataInicioEvento, tipoRequisicao, statusEvento, usuario);
            var eventos = _rastreamentoEvento.GetAllEventos(dataInicioEvento, tipoRequisicao, statusEvento, usuario, countEventos, 0);
            
            var formatado = new AssinaturaResponse<RastreamentoEventoCSV>()
            {
                Items = _mapper.Map<List<RastreamentoEventoCSV>>(eventos)//.ToList(),
            };
            _logger.LogInformation("Convertendo eventos rastreados em arquivo csv...");
            var csv = formatado.Items.ToCsv();
            return File(Encoding.ASCII.GetBytes(csv), "text/csv", "TrackingEventos.csv");
        }

        [HttpGet]
        [Route("papel")]
        public IActionResult GetPapeis([FromQuery] DateTime dataInicioEvento, [FromQuery] string idRequisicao, [FromQuery] string papel = "", [FromQuery] string statusMensagem = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var byteArrayIdRequisicao = Guid.Parse(idRequisicao);
            _logger.LogInformation("Exibindo papeis rastreados");
            dataInicioEvento = Utils.FilterDateParam(dataInicioEvento);
            var papeis = _rastreamentoPapel.GetAllPapeis(dataInicioEvento, byteArrayIdRequisicao.ToByteArray(), papel, statusMensagem, pageSize, pageNumber - 1);
            var countPapeis = _rastreamentoPapel.CountAllPapeis(dataInicioEvento, byteArrayIdRequisicao.ToByteArray(), papel, statusMensagem);

            var formatado = new AssinaturaResponse<RastreamentoPapel>()
            {
                Items = papeis.ToList(),
                HasNext = !(countPapeis - (pageSize * (pageNumber - 1)) <= pageSize)
            };

            return Ok(formatado);
        }

        [HttpGet]
        [Route("papel/csv")]
        public IActionResult GetPapeisCsv([FromQuery] DateTime dataInicioEvento, [FromQuery] string idRequisicao, [FromQuery] string papel = "", [FromQuery] string statusMensagem = "")
        {
            var byteArrayIdRequisicao = Guid.Parse(idRequisicao);
            _logger.LogInformation("Papeis rastreados em csv");
            dataInicioEvento = Utils.FilterDateParam(dataInicioEvento);
            var countPapeis = _rastreamentoPapel.CountAllPapeis(dataInicioEvento, byteArrayIdRequisicao.ToByteArray(), papel, statusMensagem);
            var papeis = _rastreamentoPapel.GetAllPapeis(dataInicioEvento, byteArrayIdRequisicao.ToByteArray(), papel, statusMensagem, countPapeis, 0);            

            var formatado = new AssinaturaResponse<RastreamentoPapelCSV>()
            {
                Items = _mapper.Map<List<RastreamentoPapelCSV>>(papeis)//.ToList(),
            };

            _logger.LogInformation("Convertendo papeis rastreados em arquivo csv...");
            var csv = formatado.Items.ToCsv();
            return File(Encoding.ASCII.GetBytes(csv), "text/csv", "TrackingPapeis.csv");
        }
    }
}
