using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Processamento.Impl.Cadastro
{
    public class CadastroHandler : AbstractProcessamentoHandler<List<DadosCaracteristicos>>
    {
        private RastreamentoMessenger _rastreamentoMessenger;

        public CadastroHandler(RastreamentoMessenger rastreamentoMessenger)
        {
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        public override List<DadosCaracteristicos> Handle(List<DadosCaracteristicos> request, EProcessStep step)
        {
            if(_rastreamentoMessenger.IsEventoIniciado())
            {
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), null, "", StatusProcessamentoEnum.PROCESSANDO, new JObject(), "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
            }
            return base.Handle(request, step);
        }
    }

    public class CadastroPersistenceHandler : AbstractProcessamentoHandler<List<DadosCaracteristicos>>
    {
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private AssinaturaRepository _assinatura;
        private IMapper _mapper;
        private RastreamentoMessenger _rastreamentoMessenger;
        private bool _forceSend;

        public CadastroPersistenceHandler(DadosCaracteristicosRepository dadosCaracteristicos, AssinaturaRepository assinaturaRepository, IMapper mapper, RastreamentoMessenger rastreamentoMessenger, bool forceSend = false)
        {
            _dadosCaracteristicos = dadosCaracteristicos;
            _assinatura = assinaturaRepository;
            _mapper = mapper;
            _rastreamentoMessenger = rastreamentoMessenger;
            _forceSend = forceSend;
        }

        public override List<DadosCaracteristicos> Handle(List<DadosCaracteristicos> request, EProcessStep step)
        {
            var dataUltimoRelatorio = _dadosCaracteristicos.GetUltimaAtualizacao();
            var allAssModif = _assinatura.GetFlagsModif(TipoImpactoEnum.CADASTRO, dataUltimoRelatorio);
            var cadastrosBPO = _dadosCaracteristicos.GetCadastros();
            var papeisImpactados = _assinatura.GetImpactados(TipoImpactoEnum.CADASTRO);
            var papeisAssinados = _assinatura.GetAllAssinados();

            var relatorioImpacto = new List<DadosCaracteristicos>();
            var relatorioFilter = new List<DadosCaracteristicos>();
            var relatorioImpactoHomolog = new List<DadosCaracteristicos>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 15
            };

            ParallelLoopResult parallelLoopResult = Parallel.ForEach(request, options, papel =>
            {
                //var logAssinatura = logsAssinatura.Where(log => log.cd_cge == papel.CodigoSNA).FirstOrDefault(); //nao preciso verificar se é null pois todo papel enviado pela luz no relatorio está assinado logo tem log
                var assModif = allAssModif.Where(ass => ass.cd_sna == papel.CodigoSNA).FirstOrDefault();
                var cadastroBPO = cadastrosBPO.Where(cadastro => cadastro.cd_sna == papel.CodigoSNA).FirstOrDefault();
                var registroImpacto = papeisImpactados.Where(impactado => impactado.cd_sna == papel.CodigoSNA).FirstOrDefault();
                //var assinatura = papeisAssinados.Where(assinado => assinado.cd_sna == papel.CodigoSNA).FirstOrDefault();
                var isInBPORel = false;

                var strictCompDtUltimaAtt = cadastroBPO is null ? false : DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) > 0;

                if (cadastroBPO is null || DateTime.Compare(papel.DataUltimaAlteracao, cadastroBPO.dt_ultima_alteracao) >= 0) //cadastro nao existe em nossa base OU precisa atualizar/foi atualizado recentemente
                {
                    if (cadastroBPO is null || strictCompDtUltimaAtt || assModif != null || _forceSend)//|| (assinatura.dt_atualizacao > cadastroBPO.dt_atualizacao))
                    {
                        relatorioFilter.Add(papel);
                        relatorioImpactoHomolog.Add(papel);
                        isInBPORel = true;
                    }

                    if (registroImpacto != null) // se esse papel está assinado e deve impactar o MDP
                    {
                        if (strictCompDtUltimaAtt || assModif != null || _forceSend)
                        {
                            relatorioImpacto.Add(papel); // adicionar dado atualizado ao BPO e enviar para o MDP
                            if (!isInBPORel)
                            {
                                relatorioFilter.Add(papel);
                            }
                        }

                    }
                }
            });

            _rastreamentoMessenger.RecebidosOnly.AddRange(_mapper.Map<List<RastreamentoPapel>>(request.Except(relatorioFilter).ToList()));
            _rastreamentoMessenger.PersistidosOnly.AddRange(_mapper.Map<List<RastreamentoPapel>>(relatorioFilter.Except(relatorioImpacto).ToList()));

            _dadosCaracteristicos.BulkInsert(_mapper.Map<List<DadosCaracteristicosDAO>>(relatorioFilter));
            var dummyData = new DadosCaracteristicos();
            dummyData.CodigoSNA = "SEPARADOR";
            relatorioImpacto.Add(dummyData);
            relatorioImpacto = relatorioImpacto.Concat(relatorioImpactoHomolog).ToList();

            IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioFilter), StatusMensagemEnum.PERSISTIDO_BPO, "");
            _rastreamentoMessenger.SendTrackingMessage(papeis);

            return base.Handle(relatorioImpacto, step);
        }
    }

    public class CadastroImpactHandler : AbstractProcessamentoHandler<List<DadosCaracteristicos>>
    {
        private ISender _rabbitSender;
        private DateTime _data;
        private IMapper _mapper;
        private RastreamentoMessenger _rastreamentoMessenger;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public CadastroImpactHandler(ISender rabbitSender, IMapper mapper, RastreamentoMessenger rastreamentoMessenger)
        {
            _rabbitSender = rabbitSender;
            _rastreamentoMessenger = rastreamentoMessenger;
            _mapper = mapper;
        }

        public override List<DadosCaracteristicos> Handle(List<DadosCaracteristicos> request, EProcessStep step)
        {
            int dummyIndex = request.Select((cadastro, index) => new { Cadastro = cadastro, Index = index }).Where((cadastro) => cadastro.Cadastro.CodigoSNA == "SEPARADOR").First().Index;
            List<DadosCaracteristicos> relatorioImpacto = request.Select((cadastro, index) => new { Cadastro = cadastro, Index = index })
                                                                .Where(cadastro => cadastro.Index < dummyIndex)
                                                                .Select(v => v.Cadastro).ToList();

            List<DadosCaracteristicos> relatorioImpactoHomolog = request.Select((cadastro, index) => new { Cadastro = cadastro, Index = index })
                                                                .Where(cadastro => cadastro.Index > dummyIndex)
                                                                .Select(v => v.Cadastro).ToList();


            //_rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", request, false, _data);
            _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpacto, false, _data, false, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeisCadastro = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpacto), StatusMensagemEnum.ENVIADO_MDP, "");
            _rastreamentoMessenger.SendTrackingMessage(papeisCadastro);

            _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpactoHomolog, false, _data, true, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeisCadastroL = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpactoHomolog), StatusMensagemEnum.ENVIADO_MDP, "");
            papeisCadastroL.ToList().ForEach((cadastroHomolog) =>
            {
                cadastroHomolog.Papel = $"L-{cadastroHomolog.Papel}";
                if (cadastroHomolog.Papel.Length > 12)
                    cadastroHomolog.Papel = cadastroHomolog.Papel.Substring(0, 12);
            });
            _rastreamentoMessenger.SendTrackingMessage(papeisCadastroL);



            return base.Handle(request, step);
        }
    }

    public class RecoveryImpactHandler : AbstractProcessamentoHandler<List<DadosCaracteristicos>>
    {
        private DadosCaracteristicosRepository _dadosCaracteristicos;
        private AssinaturaRepository _assinatura;
        private IMapper _mapper;
        private ISender _rabbitSender;
        private RastreamentoMessenger _rastreamentoMessenger;

        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public RecoveryImpactHandler(DadosCaracteristicosRepository dadosCaracteristicos, AssinaturaRepository assinaturaRepository, IMapper mapper, ISender rabbitSender, RastreamentoMessenger rastreamentoMessenger)
        {
            _dadosCaracteristicos = dadosCaracteristicos;
            _rastreamentoMessenger = rastreamentoMessenger;
            _mapper = mapper;
            _assinatura = assinaturaRepository;
            _rabbitSender = rabbitSender;

        }

        public override List<DadosCaracteristicos> Handle(List<DadosCaracteristicos> request, EProcessStep step)
        {
            var papeisImpactados = _assinatura.GetImpactados(TipoImpactoEnum.CADASTRO);

            var relatorioImpacto = new List<DadosCaracteristicos>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 15
            };

            ParallelLoopResult parallelLoopResult = Parallel.ForEach(request, options, papel =>
            {
                var registroImpacto = papeisImpactados.Where(impactado => impactado.cd_sna == papel.CodigoSNA).FirstOrDefault();

                if (registroImpacto != null) // se esse papel está assinado e deve impactar o MDP
                {
                    relatorioImpacto.Add(papel); // adicionar dado atualizado ao BPO e enviar para o MDP
                }

            });

            _rabbitSender.SendLot<DadosCaracteristicos>("DadosCaracteristicosQueue", relatorioImpacto, false, _data, false, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeisCadastro = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpacto), StatusMensagemEnum.ENVIADO_MDP, "");
            _rastreamentoMessenger.SendTrackingMessage(papeisCadastro);
            //_dadosCaracteristicos.BulkInsert(_mapper.Map<List<DadosCaracteristicosDAO>>(request));

            return base.Handle(request, step);
        }
    }

}
