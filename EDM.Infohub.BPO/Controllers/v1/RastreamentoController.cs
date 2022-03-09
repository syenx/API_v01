using AutoMapper;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static EDM.Infohub.BPO.RabbitMQ.RabbitReceiver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class RastreamentoController : ControllerBase
    {
        private ILogger<CaracteristicaController> _logger;
        private IMapper _mapper;
        private RastreamentoRepository _rastreamento;

        public RastreamentoController(ILogger<CaracteristicaController> logger, IMapper mapper, RastreamentoRepository rastreamentoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rastreamento = rastreamentoRepository;

        }
        // GET: api/<RastreamentoController>
        [HttpGet]
        public IActionResult Get([FromQuery] DateTime dh_evento)
        {
            if (dh_evento.Equals(new DateTime(0001, 01, 01)))
            {
                dh_evento = DateTime.Now;
            }
            var rastreamento = _rastreamento.GetByData(dh_evento);
            foreach (RastreamentoModel r in rastreamento)
            {
                if (r.tx_erro == null)
                {
                    r.tx_erro = "ERRO";

                }
                else
                {
                    var erro = JsonConvert.DeserializeObject<DeadLetter>(r.tx_erro);
                    r.tx_erro = erro.erro;

                }
            }

            return Ok(new Items<RastreamentoModel>() { items = (List<RastreamentoModel>)rastreamento });

        }

        // GET api/<RastreamentoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "OK";
        }

        // POST api/<RastreamentoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<RastreamentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RastreamentoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
