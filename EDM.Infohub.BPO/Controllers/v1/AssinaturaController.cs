using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models.Assinatura;
using EDM.Infohub.BPO.Models.Response;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Controllers.V1
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class AssinaturaController : ControllerBase
    {
        private readonly ILuzService _luzService;
        private readonly ILogger<InfohubLuzController> _logger;
        private AssinaturaRepository _assinatura;
        private AssinaturaFlagRepository _assinaturaFlagRepository;
        private AssinaturaLogRepository _logRepository;
        private readonly IMapper _mapper;
        private ISender _rabbitSender;
        private HangfireService _hangfire;
        private IConfiguration _configuration;
        private ICalendar _calendar;

        public AssinaturaController(ILogger<InfohubLuzController> logger, ILuzService luzService, AssinaturaRepository assinatura, AssinaturaLogRepository logRepository, IMapper mapper, AssinaturaFlagRepository assinaturaFlagRepository, ISender rabbitSender, HangfireService hangfire, IConfiguration configuration, ICalendar calendar)
        {
            _luzService = luzService;
            _logger = logger;
            _assinatura = assinatura;
            _assinaturaFlagRepository = assinaturaFlagRepository;
            _logRepository = logRepository;
            _mapper = mapper;
            _rabbitSender = rabbitSender;
            _hangfire = hangfire;
            _configuration = configuration;
            _calendar = calendar;
        }

        [HttpGet]
        //[Route("papeis-assinados")]
        public IActionResult PapeisAssinados([FromQuery] string papel = "", [FromQuery] string order = "", [FromQuery] bool? impactaPreco = null, [FromQuery] bool? impactaCadastro = null, [FromQuery] bool? impactaEvento = null, [FromQuery] bool? impactaHistorico = null, [FromQuery] DateTime? dataAssinatura = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Papeis Assinados");
            papel = papel.ToUpper();
            var totalAssinados = _assinatura.CountAssinados(papel, impactaPreco ?? null, impactaCadastro ?? null, impactaEvento ?? null, impactaHistorico ?? null, dataAssinatura ?? null);
            var papeisAssinados = _assinatura.GetAllAssinados(pageSize, page - 1, papel, order, impactaPreco ?? null, impactaCadastro ?? null, impactaEvento ?? null, impactaHistorico ?? null, dataAssinatura ?? null);

            var formatado = new AssinaturaResponse<PapelAssinado>()
            {
                Items = _mapper.Map<List<AssinaturaFlag>, List<PapelAssinado>>(papeisAssinados.ToList()),
                HasNext = !(totalAssinados - (pageSize * (page - 1)) <= pageSize)
            };
            return Ok(formatado);
        }

        [HttpGet]
        [Route("{papel}")]
        public IActionResult PapelAssinado([FromRoute] string papel)
        {
            _logger.LogInformation($"Buscando por papel {papel}");

            var obj = _assinatura.GetByHashWithFlags(Utils.GenerateHash(papel));

            if (obj.cd_sna is null)
            {
                return NotFound(new { message = $"Detalhes de assinatura de {papel} não foram encontrados." });
            }

            return Ok(_mapper.Map<AssinaturaFlag, Assinatura>(obj));
        }

        [HttpGet]
        [Route("csv")]
        public FileContentResult PapeisAssinadosCsv([FromQuery] string papel = "", [FromQuery] string order = "", [FromQuery] bool? impactaPreco = null, [FromQuery] bool? impactaCadastro = null, [FromQuery] bool? impactaEvento = null, [FromQuery] bool? impactaHistorico = null, [FromQuery] DateTime? dataAssinatura = null)
        {
            _logger.LogInformation("Papeis assinados em csv");
            papel = papel.ToUpper();
            var totalAssinados = _assinatura.CountAssinados(papel, impactaPreco ?? null, impactaCadastro ?? null, impactaEvento ?? null, impactaHistorico ?? null, dataAssinatura ?? null);
            var papeisAssinados = _assinatura.GetAllAssinados(totalAssinados, 0, papel, order, impactaPreco ?? null, impactaCadastro ?? null, impactaEvento ?? null, impactaHistorico ?? null, dataAssinatura ?? null);

            var formatado = new Items<AssinaturaObject>()
            {
                items = _mapper.Map<List<AssinaturaFlag>, List<AssinaturaObject>>(papeisAssinados.ToList())
            };
            _logger.LogInformation("Convertendo assinaturas em arquivo csv...");

            var csv = formatado.items.ToCsv();
            return File(Encoding.ASCII.GetBytes(csv), "text/csv", "AssinaturasLuz.csv");
        }

        [HttpPost]
        //[Route("assinar-papel")]
        public IActionResult AssinarPapel([FromBody] Items<AssinaturaObject> listaAssinaturas, [FromHeader] string user)
        {
            try
            {
                var listaAssinaturaLuz = _mapper.Map<List<AssinaturaObject>, List<AssinaturaLuzRequest>>(listaAssinaturas.items);
                var listaResposta = _luzService.AssinarPapelLote(listaAssinaturaLuz).Result;
                var (listaImpacto, listaAgendamentos, listaLogsAssinar) = ProcessarOperacoesEmLote(listaAssinaturas.items, listaResposta, user, true);
                AlterarFlagsAssinatura(listaLogsAssinar);
                AgendarPrecoHistoricoRequisicao(listaAgendamentos);
                return Ok(new AssinaturaResponse<AssinaturaMdp> { Items = listaImpacto, Mensagem = $"{listaImpacto.Count} assinado(s) com sucesso" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.StackTrace, instance: e.Message, statusCode: 500, title: "Erro na assinatura de ativos");

            }
        }

        [HttpPost]
        [Route("csv")]
        public IActionResult AssinarPapelCsv(IFormFile file, [FromHeader] string user)
        {
            var papeisAssinados = _assinatura.GetAllAssinados(int.MaxValue, 0, "", "", null, null, null, null, null);
            var papeisAssinadosFormatados = _mapper.Map<List<AssinaturaFlag>, List<AssinaturaMdp>>(papeisAssinados.ToList());

            var listaAssinaturas = new List<AssinaturaObject>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                _logger.LogInformation("Arquivo csv recebido.");
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        var result = new StringBuilder();
                        result.AppendLine(reader.ReadLine());
                        var items = result.ToString().Split(',');
                        if (!items[0].ToLower().Equals("papel"))
                            listaAssinaturas.Add(new AssinaturaObject
                            {
                                Papel = items[0].ToUpper(),
                                ImpactaCadastro = bool.Parse(items[1]),
                                ImpactaPreco = bool.Parse(items[2]),
                                ImpactaEvento = bool.Parse(items[3]),
                                ImpactaHistorico = bool.Parse(items[4]),
                            });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Erro ao tentar ler o arquivo csv: " + e.Message);
                        break;
                    }
                }
            }

            if (listaAssinaturas.Count.Equals(0))
            {
                var message = "Erro ao tentar ler o arquivo csv: Formato dos dados não é valido";
                _logger.LogError(message);
                return Problem(statusCode: 500, title: message);
            }

            try
            {
                var papeisDeleteAssinatura = _mapper.Map<List<AssinaturaMdp>, List<AssinaturaObject>>(papeisAssinadosFormatados.Where(x => !listaAssinaturas.Any(y => y.Papel == x.Papel)).ToList());
                var papeisNovosAssinados = listaAssinaturas.Where(x => !papeisAssinadosFormatados.Any(y => y.Papel == x.Papel)).ToList();
                var papeisModificados = listaAssinaturas.Where(x => papeisAssinadosFormatados
                    .Any(y => y.Papel == x.Papel &&
                    (y.ImpactaCadastro != x.ImpactaCadastro ||
                    y.ImpactaEvento != x.ImpactaEvento ||
                    y.ImpactaPreco != x.ImpactaPreco ||
                    y.ImpactaHistorico != x.ImpactaHistorico))).ToList();

                papeisModificados = papeisModificados.Concat(papeisNovosAssinados).ToList();

                var listaAssinaturaLuz = _mapper.Map<List<AssinaturaObject>, List<AssinaturaLuzRequest>>(papeisModificados);
                var listaResposta = _luzService.AssinarPapelLote(listaAssinaturaLuz).Result;
                var (listaImpactoAssinados, listaAgendamentos, listaLogsAssinar) = ProcessarOperacoesEmLote(papeisModificados, listaResposta, user, true);

                var listaImpactoDeletados = new List<AssinaturaMdp>();
                var listaLogsDelete = new List<AssinaturaLogInput>();
                if (!papeisDeleteAssinatura.Count.Equals(0))
                {
                    var listaAssinaturaLuzDeletados = _mapper.Map<List<AssinaturaObject>, List<AssinaturaLuzRequest>>(papeisDeleteAssinatura);
                    var listaRespostaDeletados = _luzService.RemoverPapelLote(listaAssinaturaLuzDeletados).Result;
                    (listaImpactoDeletados, _, listaLogsDelete) = ProcessarOperacoesEmLote(papeisDeleteAssinatura, listaRespostaDeletados, user, false);
                }
                AlterarFlagsAssinatura(listaLogsAssinar.Concat(listaLogsDelete).ToList());
                AgendarPrecoHistoricoRequisicao(listaAgendamentos);
              
                return Ok(new AssinaturaResponse<AssinaturaMdp> { Items = listaImpactoAssinados, Mensagem = $"{listaImpactoAssinados.Count} assinados(s) com sucesso e {listaImpactoDeletados.Count} deletados(s) com sucesso" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.StackTrace, instance: e.Message, statusCode: 500, title: "Erro na assinatura de ativos");

            }
            
        }

        [HttpDelete]
        public IActionResult RemoverPapelLote([FromBody] Items<AssinaturaObject> listaAssinaturas, [FromHeader] string user)
        {
            try
            {
                var listaAssinaturaLuz = _mapper.Map<List<AssinaturaObject>, List<AssinaturaLuzRequest>>(listaAssinaturas.items);
                var listaResposta = _luzService.RemoverPapelLote(listaAssinaturaLuz).Result;
                var (listaImpacto, _, listaLogsDelete) = ProcessarOperacoesEmLote(listaAssinaturas.items, listaResposta, user, false);
                AlterarFlagsAssinatura(listaLogsDelete);

                return Ok(new AssinaturaResponse<AssinaturaMdp> { Items = listaImpacto, Mensagem = $"{listaImpacto.Count} removido(s) com sucesso" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(detail: e.StackTrace, instance: e.Message, statusCode: 500, title: "Erro na assinatura de ativos");

            }
        }

        [HttpGet]
        [Route("log")]
        public IActionResult PapeisAssinados([FromQuery] string papel)
        {
            _logger.LogInformation($"Requerindo o log {papel}");

            var logPapel = _logRepository.GetLogPapel(papel);

            if (logPapel.Count() <= 0)
            {
                return NotFound(new { message = $"Nenhum log encontrado para o papel {papel}" });
            }

            var items = new Items<AssinaturaLogResponse>() { items = _mapper.Map<List<AssinaturaLogDAO>, List<AssinaturaLogResponse>>(logPapel.ToList()) };
            return Ok(items);
        }

        private (AssinaturaMdp, AssinaturaLogInput) MontarObjetoAssinatura(AssinaturaDAO obj, bool es_assinado, AssinaturaObject impactos, string user, byte[] hash, string papel)
        {
            var objLog = new AssinaturaLogDAO();
            if (obj.pk_assinaturas == null)
            {

                obj.cd_sna_hash = hash;
                obj.es_assinado = es_assinado;
                obj.cd_sna = papel.ToUpper().Trim();
                obj.dt_assinatura = DateTime.Now;
                obj.dt_atualizacao = DateTime.Now;
                obj.impacta_mdp = impactos.ImpactaPreco;

                (obj,objLog) = _assinatura.Insert(obj, impactos, user);

            }
            else
            {
                //papel já foi assinado mas agora está dessasinado
                if (!(obj.es_assinado == es_assinado))
                {
                    obj.es_assinado = es_assinado;
                    obj.dt_assinatura = DateTime.Now;
                }
                obj.dt_atualizacao = DateTime.Now;
                obj.impacta_mdp = impactos.ImpactaPreco;
                (_, objLog) = _assinatura.Update(obj, impactos, user);
            }

            if (!es_assinado)
            {
                impactos.ImpactaPreco = false;
                impactos.ImpactaCadastro = false;
                impactos.ImpactaEvento = false;
                impactos.ImpactaHistorico = false;
            }

            var objFlag = _assinatura.GetByHashWithFlags(obj.cd_sna_hash);
            var newData = false;
            if(objFlag.pk_assinaturas == null) //registro de flags nao existe na base
            {
                objFlag = new AssinaturaFlag()
                {
                    pk_assinaturas = obj.pk_assinaturas,
                    cd_sna = obj.cd_sna,
                    cd_sna_hash = obj.cd_sna_hash,
                    dt_assinatura = obj.dt_assinatura,
                    dt_atualizacao = obj.dt_atualizacao,
                    es_assinado = obj.es_assinado,
                    impacta_mdp = false,
                    impacta_cadastro = false,
                    impacta_preco = false,
                    impacta_pu_evento = false,
                    impacta_pu_historico = false
                };
                newData = true;
                //MontarObjetoAssinaturaFlag(objFlag, impactos, true);
            }
            //else
            //{
            //    MontarObjetoAssinaturaFlag(objFlag, impactos);
            //}

            return (new AssinaturaMdp()
            {
                Papel = impactos.Papel,
                ImpactaPreco = impactos.ImpactaPreco,
                ImpactaCadastro = impactos.ImpactaCadastro,
                ImpactaEvento = impactos.ImpactaEvento,
                ImpactaHistorico = impactos.ImpactaHistorico,
                Assinado = obj.es_assinado
            },
            new AssinaturaLogInput()
            {
                logObject = objLog,
                flagObject = objFlag,
                impactos = impactos,
                newData = newData
            });
        }

        private void AlterarFlagsAssinatura(List<AssinaturaLogInput> logs)//AssinaturaFlag obj, AssinaturaObject impactos, bool newData = false)
        {
            Task.Run(() =>
            {
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 15
                };

                ParallelLoopResult parallelLoopResult = Parallel.ForEach(logs, options, log =>
                {
                    var obj = log.flagObject;
                    var impactos = log.impactos;
                    var newData = log.newData;

                    //Checando se as flags foram modificadas
                    var changedPreco = obj.impacta_preco != impactos.ImpactaPreco ? true : false;
                    var changedCadastro = obj.impacta_cadastro != impactos.ImpactaCadastro ? true : false;
                    var changedPU = obj.impacta_pu_evento != impactos.ImpactaEvento ? true : false;
                    var changedPHist = obj.impacta_pu_historico != impactos.ImpactaHistorico ? true : false;

                    var flags = new List<AssinaturaFlagDAO>();

                    if (changedPreco || newData)
                    {
                        var flagpreco = new AssinaturaFlagDAO()
                        {
                            FK_ASSINATURA = (int)obj.pk_assinaturas,
                            EN_TIPO_IMPACTO = "PRECO",
                            ES_IMPACTADO = impactos.ImpactaPreco,
                            dt_atualizacao_flag = DateTime.Now
                        };
                        flags.Add(flagpreco);
                    }
                    if (changedCadastro || newData)
                    {
                        var flagcadastro = new AssinaturaFlagDAO()
                        {
                            FK_ASSINATURA = (int)obj.pk_assinaturas,
                            EN_TIPO_IMPACTO = "CADASTRO",
                            ES_IMPACTADO = impactos.ImpactaCadastro,
                            dt_atualizacao_flag = DateTime.Now
                        };
                        flags.Add(flagcadastro);
                    }
                    if (changedPU || newData)
                    {
                        var flagPuEvento = new AssinaturaFlagDAO()
                        {
                            FK_ASSINATURA = (int)obj.pk_assinaturas,
                            EN_TIPO_IMPACTO = "PU_EVENTO",
                            ES_IMPACTADO = impactos.ImpactaEvento,
                            dt_atualizacao_flag = DateTime.Now
                        };
                        flags.Add(flagPuEvento);
                    }
                    if (changedPHist || newData)
                    {
                        var flagPUHistorico = new AssinaturaFlagDAO()
                        {
                            FK_ASSINATURA = (int)obj.pk_assinaturas,
                            EN_TIPO_IMPACTO = "PU_HISTORICO",
                            ES_IMPACTADO = impactos.ImpactaHistorico,
                            dt_atualizacao_flag = DateTime.Now
                        };
                        flags.Add(flagPUHistorico);
                    }
                    flags.ForEach((f) =>
                    {
                        _assinaturaFlagRepository.InsertUpdate(f);
                    });

                    var rowLogAssinatura = log.logObject;
                    _logRepository.Insert(rowLogAssinatura);

                });
              
            });
            RelatorioDiaMdp();
        }

        [HttpGet]
        [Route("mdp")]
        public IActionResult RelatorioDiaMdp()
        {
            try
            {
                _logger.LogInformation("Buscando assinaturas");

                var papeisAssinados = _assinatura.GetAllAssinados(int.MaxValue, 0, "", "", null, null, null, null, null);
                var papeisAssinadosFormatados = _mapper.Map<List<AssinaturaFlag>, List<AssinaturaMdp>>(papeisAssinados.ToList());

                _rabbitSender.SendBulk<AssinaturaMdp>("AssinaturasQueue", papeisAssinadosFormatados, true);
                return Ok(papeisAssinadosFormatados);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(detail: e.StackTrace, instance: e.Message, statusCode: 500, title: "Erro na geracao e/ou impacto de relatorio dia de assinaturas");
            }
        }

        private void AgendarPrecoHistoricoRequisicao(List<AssinaturaLuzResponse> papeis)
        {
            Task.Run(async () =>
            {
                foreach (AssinaturaLuzResponse papel in papeis)
                {
                    var dateNow = DateTime.Now;


                    var nextDate = _calendar.GetNextDay(new CalendarService.CalendarRequestCoppClark()
                    {
                        BeginDate = DateTime.Now,
                        QttWorkDays = 1,
                        SourceId = 142,

                    });


                    var diference = nextDate.AddHours(12) - dateNow;


                    _ = _hangfire.AgendaTemporizador($"{_configuration["InfohubAPIUrl"]}v1/Historico/{papel.Papel}", (int)diference.TotalMinutes);
                }             

            });
            
            
        }

        private (List<AssinaturaMdp>, List<AssinaturaLuzResponse>, List<AssinaturaLogInput>) ProcessarOperacoesEmLote(List<AssinaturaObject> listaAssinaturas, List<AssinaturaLuzResponse> listaResposta, string user, bool es_assinado)
        {
            string messageErro = "";
            string operation = es_assinado ? "Insert" : "Delete";
            var listaImpacto = new List<AssinaturaMdp>();
            var listaAgendaHistorico = new List<AssinaturaLuzResponse>();
            var listaImpactaLog = new List<AssinaturaLogInput>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 15
            };

            ParallelLoopResult parallelLoopResult = Parallel.ForEach(listaResposta, options, papel =>
            {
                if (!papel.Mensagem.Contains("Erro"))
                {
                    var hash = Utils.GenerateHash(papel.Papel);

                    var obj = _assinatura.GetByHash(hash);
                    bool estavaAssinado = obj.es_assinado;

                    var impactaMdp = listaAssinaturas.Where(p => p.Papel == papel.Papel).FirstOrDefault();

                    var (objectoImpactoMontado, objetoLogMontado) = MontarObjetoAssinatura(obj, es_assinado, impactaMdp, user, hash, papel.Papel);
                    listaImpacto.Add(objectoImpactoMontado);
                    listaImpactaLog.Add(objetoLogMontado);

                    if (!estavaAssinado && es_assinado)
                    {
                        listaAgendaHistorico.Add(papel);
                    }
                }
                else
                {
                    _logger.LogError($"{operation} do papel {papel.Papel} não foi executado com sucesso");
                    messageErro += $"{operation} do papel {papel.Papel} não foi executado com sucesso/";

                }
            });

            if (messageErro != "")
            {
                throw new Exception(messageErro);
            }

            //fireforget
            //SaveAssinaturaLogs(listaImpactaLog);
            //AgendarPrecoHistoricoRequisicao(listaAgendaHistorico);            

            return (listaImpacto, listaAgendaHistorico, listaImpactaLog);
        }
    }
}