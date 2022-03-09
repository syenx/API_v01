using AutoMapper;
using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.Processamento.Impl.Fluxo
{
    public class FluxoHandler : AbstractProcessamentoHandler<List<Fluxos>>
    {
        private RastreamentoMessenger _rastreamentoMessenger;

        public FluxoHandler(RastreamentoMessenger rastreamentoMessenger)
        {
            _rastreamentoMessenger = rastreamentoMessenger;
        }

        public override List<Fluxos> Handle(List<Fluxos> request, EProcessStep step)
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

    public class FluxoPersistenceHandler : AbstractProcessamentoHandler<List<Fluxos>>
    {
        private FluxosRepository _fluxosPersistence;
        private IMapper _mapper;

        public FluxoPersistenceHandler(FluxosRepository fluxosRepository, IMapper mapper)
        {
            _fluxosPersistence = fluxosRepository;
            _mapper = mapper;
        }

        public override List<Fluxos> Handle(List<Fluxos> request, EProcessStep step)
        {
            _fluxosPersistence.BulkInsert(_mapper.Map<List<FluxosDAO>>(request));

            return base.Handle(request, step);
        }
    }
}
