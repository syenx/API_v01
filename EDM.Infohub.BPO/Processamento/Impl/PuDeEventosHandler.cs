using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.DataAccess.Impl;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.DadosAtuais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;

namespace EDM.Infohub.BPO.Processamento.Impl
{
    public class PuDeEventosHandler : AbstractProcessamentoHandler<List<PuDeEventos>>
    {
        private RastreamentoMessenger _rastreamentoMessenger;
        public PuDeEventosHandler(RastreamentoMessenger rastreamentoMessenger)
        {
            _rastreamentoMessenger = rastreamentoMessenger;
        }
        public override List<PuDeEventos> Handle(List<PuDeEventos> request, EProcessStep step)
        {
            if (_rastreamentoMessenger.IsEventoIniciado())
            {
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), null, "", StatusProcessamentoEnum.PROCESSANDO, new JObject(), "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
            }
            return base.Handle(request, step);
        }
    }
    public class PuDeEventosImpactHandler : AbstractProcessamentoHandler<List<PuDeEventos>>
    {
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IMapper _mapper;
        private IFireForgetRepositoryHandler _fireForget;
        private RastreamentoMessenger _rastreamentoMessenger;
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public PuDeEventosImpactHandler(ISender rabbitSender, AssinaturaRepository assinaturaRepository, RastreamentoMessenger rastreamentoMessenger, IMapper mapper, IFireForgetRepositoryHandler fireForget)
        {
            _rabbitSender = rabbitSender;
            _assinatura = assinaturaRepository;
            _rastreamentoMessenger = rastreamentoMessenger;
            _mapper = mapper;
            _fireForget = fireForget;
        }

        public override List<PuDeEventos> Handle(List<PuDeEventos> request, EProcessStep step)
        {
            var relatorioImpacto = new List<PuDeEventos>();

            foreach (PuDeEventos papel in request)
            {
                if (_assinatura.PapelImpactado(Utils.GenerateHash(papel.CodigoSNA), TipoImpactoEnum.PU_EVENTO))
                {
                    relatorioImpacto.Add(papel);
                    _fireForget.ExecuteCadastro(async fluxos =>
                    {
                        // Will receive its own scoped repository on the executing task
                        fluxos.RelatorioCaracteristicaDia("",DateTime.Now,papel.CodigoSNA);
                    });
                }
            };

            _rastreamentoMessenger.PersistidosOnly.AddRange(_mapper.Map<List<RastreamentoPapel>>(request.Except(relatorioImpacto).ToList()));

            _rabbitSender.SendLot("PuDeEventosQueue", relatorioImpacto, true, _data, false, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeisPU = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpacto), StatusMensagemEnum.ENVIADO_MDP, "");
            _rastreamentoMessenger.SendTrackingMessage(papeisPU);

            _rabbitSender.SendLot("PuDeEventosQueue", request, true, _data, true, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeisPUL = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(request), StatusMensagemEnum.ENVIADO_MDP, "");
            papeisPUL.ToList().ForEach((puHomolog) =>
            {
                puHomolog.Papel = $"L-{puHomolog.Papel}";
                if (puHomolog.Papel.Length > 12)
                    puHomolog.Papel = puHomolog.Papel.Substring(0, 12);
            });
            _rastreamentoMessenger.SendTrackingMessage(papeisPUL);

            return base.Handle(request, step);
        }
    }

    public class PuDeEventosPersistenceHandler : AbstractProcessamentoHandler<List<PuDeEventos>>
    {
        private PuDeEventosRepository _puDeEventosPersistence;
        private RastreamentoMessenger _rastreamentoMessenger;
        private AssinaturaRepository _assinatura;
        private IMapper _mapper;
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public PuDeEventosPersistenceHandler(PuDeEventosRepository puDeEventosRepository, IMapper mapper, RastreamentoMessenger rastreamentoMessenger, AssinaturaRepository assinaturaRepository)
        {
            _puDeEventosPersistence = puDeEventosRepository;
            _mapper = mapper;
            _rastreamentoMessenger = rastreamentoMessenger;
            _assinatura = assinaturaRepository;
        }

        public override List<PuDeEventos> Handle(List<PuDeEventos> request, EProcessStep step)
        {
            var pusArmazenado = _puDeEventosPersistence.GetByData(Data);
            List<PuDeEventos> PuDeEventosImpacto = new List<PuDeEventos>();
            var flagPapeisImpactados = _assinatura.GetImpactados(TipoImpactoEnum.PU_EVENTO);
            foreach (PuDeEventos puEvento in request)
            {
                var evento = pusArmazenado.Where(pu => pu.cd_sna == puEvento.CodigoSNA).FirstOrDefault();
                var registroImpacto = flagPapeisImpactados.Where(impactado => impactado.cd_sna == puEvento.CodigoSNA).FirstOrDefault();
                if (evento != null)
                {
                    if(evento.dt_att_status_pgto < puEvento.DataAttStatusPgto)
                    {
                        PuDeEventosImpacto.Add(puEvento);
                    }
                    else if(registroImpacto != null)
                    {
                        if(evento.dt_criacao < registroImpacto.dt_atualizacao)
                            PuDeEventosImpacto.Add(puEvento);
                    }
                }
                else
                    PuDeEventosImpacto.Add(puEvento);
            }

            _rastreamentoMessenger.RecebidosOnly.AddRange(_mapper.Map<List<RastreamentoPapel>>(request.Except(PuDeEventosImpacto).ToList()));
            _puDeEventosPersistence.BulkInsert(_mapper.Map<List<PuDeEventosDAO>>(PuDeEventosImpacto), _data);

            IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(PuDeEventosImpacto), StatusMensagemEnum.PERSISTIDO_BPO, "");
            _rastreamentoMessenger.SendTrackingMessage(papeis);

            return base.Handle(PuDeEventosImpacto, step);
        }
    }
}
