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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
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

        public CaracteristicaController(ILogger<CaracteristicaController> logger, ILuzService luzService, RawDataRepository rawData, IMapper mapper, DadosCaracteristicosRepository dadosCaracteristicos, AssinaturaRepository assinaturaRepository, ISender rabbitSender, IFireForgetRepositoryHandler fireForget, HangfireService hangfire, AssinaturaLogRepository logRepository, IConfiguration configuration)
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
        }

        [HttpGet]
        [Route("relatorio-dia")]
        public IActionResult RelatorioCaracteristicaDia([FromQuery] DateTime data, [FromQuery] int tentativa = 0)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                data = DateTime.Now;
            }
            _logger.LogInformation("Buscando relatorio de caracteristica do dia");



            var relatorio = _luzService.RelatorioCaracteristica(data).Result;

            if (relatorio.Count.Equals(0) && tentativa < 5)
            {
                tentativa++;
                var agendar = _hangfire.AgendaTemporizador($"{_config["InfohubAPIUrl"]}v1/Caracteristica/relatorio-dia?tentativa={tentativa}", 10);
                _logger.LogInformation("Não foi encontrado relatório de preços do dia " + data);
                _ = agendar.Result;
                return NoContent();
            }
            else if (tentativa == 4)
            {
                _logger.LogError($"Tentativas de requerir o relátório de preços excedida(5), contate a Luz Sistemas para entender o atraso");
                return Problem(detail: $"Tentativas de requerir o relátório de preços excedida(5), contate a Luz Sistemas para entender o atraso", statusCode: 500);
            }

            if (relatorio.Count.Equals(0))
            {
                _logger.LogInformation("Nao foi encontrado relatório de caracteristica do dia " + data);
                return NoContent();
            }

            try
            {
                var relatorioImpacto = new List<DadosCaracteristicos>();
                var relatorioFilter = new List<DadosCaracteristicos>();
                var relatorioImpactoHomolog = new List<DadosCaracteristicos>();

                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 15
                };

                Parallel.Invoke(options, () => { _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio)); },
                    () =>
                    {
                        //var logsAssinatura = _asslogRepository.GetLastLogPapeis();
                        var dataUltimoRelatorio = _dadosCaracteristicos.GetUltimaAtualizacao();
                        var allAssModif = _assinatura.GetFlagsModif(TipoImpactoEnum.CADASTRO, dataUltimoRelatorio);
                        var cadastrosBPO = _dadosCaracteristicos.GetCadastros();
                        var papeisImpactados = _assinatura.GetImpactados(TipoImpactoEnum.CADASTRO);
                        var papeisAssinados = _assinatura.GetAllAssinados();

                        Parallel.ForEach(relatorio, options, papel =>
                        {
                            //var logAssinatura = logsAssinatura.Where(log => log.cd_cge == papel.CodigoSNA).FirstOrDefault(); //nao preciso verificar se é null pois todo papel enviado pela luz no relatorio está assinado logo tem log
                            var assModif = allAssModif.Where(ass => ass.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var cadastroBPO = cadastrosBPO.Where(cadastro => cadastro.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var registroImpacto = papeisImpactados.Where(impactado => impactado.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var assinatura = papeisAssinados.Where(assinado => assinado.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var isInBPORel = false;

                            var strictCompDtUltimaAtt = cadastroBPO is null ? false : DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) > 0;

                            if (cadastroBPO is null || DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) >= 0) //cadastro nao existe em nossa base OU precisa atualizar/foi atualizado recentemente
                            {
                                if (cadastroBPO is null || strictCompDtUltimaAtt || assModif != null || (assinatura.dt_atualizacao > cadastroBPO.dt_atualizacao))
                                {
                                    relatorioFilter.Add(papel);
                                    relatorioImpactoHomolog.Add(papel);
                                    isInBPORel = true;
                                }

                                if (registroImpacto != null) // se esse papel está assinado e deve impactar o MDP
                                {
                                    if (strictCompDtUltimaAtt || assModif != null)
                                    {
                                        relatorioImpacto.Add(papel); // adicionar dado atualizado ao BPO e enviar para o MDP
                                        if (!isInBPORel)
                                        {
                                            relatorioFilter.Add(papel);
                                        }
                                    }                                        
                                }
                            }
                        }
                        );
                    });

                _dadosCaracteristicos.BulkInsert(_mapper.Map<List<DadosCaracteristicosDAO>>(relatorioFilter));
                _logger.LogInformation($"{relatorioImpacto.Count()} enviados para impacto no MDP");
                _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpacto, false, data, false);
                _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpactoHomolog, false, data, true);

                //fireforget

                //_fireForget.ExecuteFluxos(async fluxos =>
                //{
                //    // Will receive its own scoped repository on the executing task
                //    await fluxos.RelatorioFluxoDia(data);
                //});

            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(relatorio);
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
        public IActionResult RelatorioCaracteristicaDia([FromQuery] DateTime data, [FromRoute] string papel)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                data = DateTime.Now;
            }
            _logger.LogInformation("Buscando relatorio de caracteristica do dia");

            var relatorio = _luzService.RelatorioCaracteristicaPapel(data, papel).Result;
            var _messages = new List<object>();

            if (relatorio.Count.Equals(0))
            {
                _logger.LogInformation("Nao foi encontrado relatório de caracteristica do dia " + data);
                return NoContent();
            }

            try
            {
                var relatorioImpacto = new List<DadosCaracteristicos>();
                var relatorioFilter = new List<DadosCaracteristicos>();
                var relatorioImpactoHomolog = new List<DadosCaracteristicos>();

                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 15
                };

                Parallel.Invoke(() => { _rawData.BulkInsert(_mapper.Map<List<RawDataEventosProcessadosDAO>>(relatorio)); },
                    () =>
                    {
                        //var logsAssinatura = _asslogRepository.GetLastLogPapeis();
                        var dataUltimoRelatorio = _dadosCaracteristicos.GetUltimaAtualizacao();
                        var allAssModif = _assinatura.GetFlagsModif(TipoImpactoEnum.CADASTRO, dataUltimoRelatorio);
                        var cadastrosBPO = _dadosCaracteristicos.GetCadastros();
                        var papeisImpactados = _assinatura.GetImpactados(TipoImpactoEnum.CADASTRO);
                        var papeisAssinados = _assinatura.GetAllAssinados();

                        Parallel.ForEach(relatorio, options, papel =>
                        {
                            //var logAssinatura = logsAssinatura.Where(log => log.cd_cge == papel.CodigoSNA).FirstOrDefault(); //nao preciso verificar se é null pois todo papel enviado pela luz no relatorio está assinado logo tem log
                            var assModif = allAssModif.Where(ass => ass.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var cadastroBPO = cadastrosBPO.Where(cadastro => cadastro.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var registroImpacto = papeisImpactados.Where(impactado => impactado.cd_sna == papel.CodigoSNA).FirstOrDefault();
                            var isInBPORel = false;
                            var assinatura = papeisAssinados.Where(assinado => assinado.cd_sna == papel.CodigoSNA).FirstOrDefault();

                            var strictCompDtUltimaAtt = cadastroBPO is null ? false : DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) > 0;

                            if (cadastroBPO is null || DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) >= 0) //cadastro nao existe em nossa base OU precisa atualizar/foi atualizado recentemente
                            {
                                if (cadastroBPO is null || strictCompDtUltimaAtt || assModif != null || (assinatura.dt_atualizacao > cadastroBPO.dt_atualizacao))
                                {
                                    relatorioFilter.Add(papel);
                                    relatorioImpactoHomolog.Add(papel);
                                    isInBPORel = true;
                                }

                                if (registroImpacto != null) // se esse papel está assinado e deve impactar o MDP
                                {
                                    if (strictCompDtUltimaAtt || assModif != null)
                                    {
                                        relatorioImpacto.Add(papel); // adicionar dado atualizado ao BPO e enviar para o MDP
                                        if (!isInBPORel)
                                        {
                                            relatorioFilter.Add(papel);
                                        }
                                    }
                                }
                            }
                        }
                        );
                    });

                _dadosCaracteristicos.BulkInsert(_mapper.Map<List<DadosCaracteristicosDAO>>(relatorioFilter));
                _logger.LogInformation($"{relatorioImpacto.Count()} enviados para impacto no MDP");
                _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpacto, false, data, false);
                _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpactoHomolog, false, data, true);

                //fireforget

                //_fireForget.ExecuteFluxos(async fluxos =>
                //{
                //    // Will receive its own scoped repository on the executing task
                //    await fluxos.RelatorioFluxoDiaUnitario(data, papel);
                //});

                var tempmsgMDP = relatorioImpacto.Count().Equals(0) ? $"O papel {papel} não foi modificado no MDP" : $"O papel {papel} foi modificado no MDP";
                var tempmsgBPO = relatorioFilter.Count().Equals(0) ? $"O papel {papel} não foi modificado no BPO" : $"O papel {papel} foi modificado no BPO";
                _messages.Add(new { message = tempmsgMDP });
                _messages.Add(new { message = tempmsgBPO });
                _messages.Add(relatorio);

            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(_messages);
        }
    }
}
