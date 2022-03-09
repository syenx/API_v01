using Amazon.SecretsManager;
using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace EDM.Infohub.BPO.Processamento.Impl.Preco
{
    public class PrecoHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        private RastreamentoMessenger _rastreamentoMessenger;

        public PrecoHandler(RastreamentoMessenger rastreamentoMessenger)
        {
            _rastreamentoMessenger = rastreamentoMessenger;
        }
        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {
            if (_rastreamentoMessenger.IsEventoIniciado())
            {
                var mensagemEvento = _rastreamentoMessenger.MontarObjetoEvento(new TipoRequisicaoEnum(), null, "", StatusProcessamentoEnum.PROCESSANDO, new JObject(), "");
                _rastreamentoMessenger.SendTrackingMessage(mensagemEvento);
            }
            //logica
            return base.Handle(request, step);
        }
    }

    public class PrecosPersistenceHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        private PrecosRepository _precosPersistence;
        private IMapper _mapper;
        private IAmazonSecretsManager _secret;
        private RastreamentoMessenger _rastreamentoMessenger;
        private IConfiguration _config;
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public PrecosPersistenceHandler(PrecosRepository precosRepository, IMapper mapper, IConfiguration config, IAmazonSecretsManager secret, RastreamentoMessenger rastreamentoMessenger )
        {
            _precosPersistence = precosRepository;
            _mapper = mapper;
            _secret = secret;
            _config = config;
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {
            Task.Run(async () =>
            {
                var _precosPersistence = new PrecosRepository(_config, _secret);
                _precosPersistence.BulkInsert(_mapper.Map<List<DadosPrecoDAO>>(request), _data);
            });

            if (_rastreamentoMessenger.EventoEmAndamento.TipoRequisicao != TipoRequisicaoEnum.PRECO_HISTORICO.ToString())
            {
                IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(request), StatusMensagemEnum.PERSISTIDO_BPO, "");
                _rastreamentoMessenger.SendTrackingMessage(papeis);
            }

            return base.Handle(request, step);
        }
    }

    public class PrecosImpactHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private IMapper _mapper;
        private RastreamentoMessenger _rastreamentoMessenger;

        public PrecosImpactHandler(ISender rabbitSender, AssinaturaRepository assinaturaRepository, IMapper mapper, RastreamentoMessenger rastreamentoMessenger)
        {
            _rabbitSender = rabbitSender;
            _assinatura = assinaturaRepository;
            _mapper = mapper;
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {

            var relatorioImpacto = new List<DadosPrecoLuz>();

            foreach (DadosPrecoLuz papel in request)
            {
                if (_assinatura.PapelImpactado(Utils.GenerateHash(papel.CodigoSNA), TipoImpactoEnum.PRECO))
                {
                    relatorioImpacto.Add(papel);
                }
            };

            _rastreamentoMessenger.PersistidosOnly.AddRange(_mapper.Map<List<RastreamentoPapel>>(request.Except(relatorioImpacto).ToList()));

            _rabbitSender.SendLot<DadosPrecoLuz>("PrecoQueue", relatorioImpacto, true, new DateTime(), false, _rastreamentoMessenger.EventoEmAndamento);
            IEnumerable<RastreamentoPapel> papeis = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpacto), StatusMensagemEnum.ENVIADO_MDP, "");
            _rastreamentoMessenger.SendTrackingMessage(papeis);

            return base.Handle(request, step);
        }
    }

    public class PrecosHistoricoImpactHandler : AbstractProcessamentoHandler<List<DadosPrecoLuz>>
    {
        private ISender _rabbitSender;
        private AssinaturaRepository _assinatura;
        private RastreamentoMessenger _rastreamentoMessenger;
        private IMapper _mapper;

        public PrecosHistoricoImpactHandler(ISender rabbitSender, AssinaturaRepository assinaturaRepository, RastreamentoMessenger rastreamentoMessenger, IMapper mapper)
        {
            _rabbitSender = rabbitSender;
            _assinatura = assinaturaRepository;
            _rastreamentoMessenger = rastreamentoMessenger;
            _mapper = mapper;
        }

        public override List<DadosPrecoLuz> Handle(List<DadosPrecoLuz> request, EProcessStep step)
        {
            var relatorioImpacto = new List<DadosPrecoLuz>();

            foreach (DadosPrecoLuz papel in request)
            {
                if (_assinatura.PapelImpactado(Utils.GenerateHash(papel.CodigoSNA), TipoImpactoEnum.PU_HISTORICO))
                {
                    relatorioImpacto.Add(papel);
                }
            };

            _rabbitSender.SendLot("PrecoHistoricoQueue", relatorioImpacto, true, new DateTime(), false, _rastreamentoMessenger.EventoEmAndamento);
            //IEnumerable<RastreamentoPapel> papeisHist = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(relatorioImpacto), StatusMensagemEnum.ENVIADO_MDP, "");
            //_rastreamentoMessenger.SendTrackingMessage(papeisHist);
            _rabbitSender.SendLot("PrecoHistoricoQueue", request, true, new DateTime(), true, _rastreamentoMessenger.EventoEmAndamento);
            //IEnumerable<RastreamentoPapel> papeisHistL = _rastreamentoMessenger.MontarObjetoPapel(_mapper.Map<List<RastreamentoPapel>>(request), StatusMensagemEnum.ENVIADO_MDP, "");
            //papeisHistL.ToList().ForEach((histHomolog) =>
            //{
            //    histHomolog.Papel = $"L-{histHomolog.Papel}";
            //    if (histHomolog.Papel.Length > 12)
            //        histHomolog.Papel = histHomolog.Papel.Substring(0, 12);
            //});
            //_rastreamentoMessenger.SendTrackingMessage(papeisHistL);


            return base.Handle(request, step);
        }
    }
}
