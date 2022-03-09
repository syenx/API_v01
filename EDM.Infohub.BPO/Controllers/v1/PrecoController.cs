using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using EDM.Infohub.BPO.Models.DadosAtuais;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
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

        public PrecoController(ILogger<PrecoController> logger, IConfiguration config, ILuzService luzService, RawDataRepository rawData, AssinaturaRepository assinaturaRepository, IMapper mapper, PrecosRepository preco, ISender rabbitSender, HangfireService hangfire)
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
        }

        [HttpGet]
        public IActionResult RelatorioDiaFront([FromQuery] DateTime dataCriacao, [FromQuery] string horaCriacao, [FromQuery] string search = "", [FromQuery] string codigoSNA = "", [FromQuery] string tipo = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            TimeSpan timeCriacao = Utils.FilterTimeParam(horaCriacao);
            dataCriacao = Utils.FilterDateParam(dataCriacao);
            _logger.LogInformation("Buscando preços do dia para a tabela do portal");

            var totalPapeis = _precos.CountFiltered(dataCriacao, timeCriacao, search.Length.Equals(0) ? codigoSNA : search, tipo);
            var relatorio = _precos.GetFiltered(dataCriacao, timeCriacao, search.Length.Equals(0) ? codigoSNA : search, tipo, pageSize, page - 1);

            var relatorioFormatado = new ListResponse<DadosPrecoLuz>()
            {
                Items = _mapper.Map<List<DadosPrecoDAO>, List<DadosPrecoLuz>>(relatorio.ToList()),
                HasNext = !(totalPapeis - (pageSize * (page - 1)) <= pageSize)
            };

            return Ok(relatorioFormatado);
        }


        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioDia([FromQuery] DateTime data, [FromQuery] int tentativa = 0)
        {
            data = Utils.FilterDateParam(data);
            _logger.LogInformation("Buscando relatório de preços do dia");
            //PROCESSANDO
            List<DadosPrecoLuz> relatorio = null;
            try
            {
                (relatorio, _) = _luzService.RelatorioPreco(data, null).Result;
                //RECEBIDO LUZ
            }
            catch (Exception e)
            {
                throw e;
            }

            if (relatorio.Count.Equals(0) && tentativa < 5)
            {
                tentativa++;
                var agendar = _hangfire.AgendaTemporizador($"{_config["InfohubAPIUrl"]}v1/Preco/relatorio-dia?tentativa={tentativa}", 10);
                _logger.LogInformation("Não foi encontrado relatório de preços do dia " + data);
                _ = agendar.Result;
                return NoContent();
            }
            else if (tentativa == 4)
            {
                _logger.LogError($"Tentativas de requerir o relátório de preços excedida(5), contate a Luz Sistemas para entender o atraso");
                return Problem(detail: $"Tentativas de requerir o relátório de preços excedida(5), contate a Luz Sistemas para entender o atraso", statusCode: 500);
            }

            try
            {
                _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio));

                _precos.BulkInsert(_mapper.Map<List<DadosPrecoDAO>>(relatorio), data);

                var relatorioImpacto = new List<DadosPrecoLuz>();

                foreach (DadosPrecoLuz papel in relatorio)
                {
                    if (_assinatura.PapelImpactado(Utils.GenerateHash(papel.CodigoSNA), TipoImpactoEnum.PRECO))
                    {
                        relatorioImpacto.Add(papel);
                    }
                };

                _rabbitSender.SendLot<DadosPrecoLuz>("PrecoQueue", relatorioImpacto, true);
            }
            catch (Exception e)
            {
                throw e;
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
        public FileContentResult RelatorioPapelPrecosCsv([FromQuery] DateTime data, [FromQuery] string horaCriacao, [FromQuery] string codigoSNA = "", [FromQuery] string tipo = null)
        {
            TimeSpan timeCriacao = Utils.FilterTimeParam(horaCriacao);
            data = Utils.FilterDateParam(data);
            _logger.LogInformation("Convertendo relatorio de preços em arquivo csv...");
            _logger.LogInformation("Buscando relatório de preços do dia");

            //var totalPapeis = _precos.CountFiltered(data, timeCriacao, "",null);
            var relatorio = _precos.GetFiltered(data, timeCriacao, codigoSNA, tipo);

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
