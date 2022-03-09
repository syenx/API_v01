using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.SQS;
using EDM.Infohub.BPO.SQS.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Events
{
    public class RastreamentoMessenger
    {
        //public event EventHandler<EventoDetectedEventArgs> RegisterDetected;
        //public event EventHandler<EventoDetectedEventArgs> EventoDetected;
        //public event EventHandler<EventoDetectedEventArgs> PapelDetected;
        public bool HouvePersistenciaBPO = false;
        public bool HouveEnvioMDP = false;
        public bool HouveFalha = false;
        private readonly ISendSQSQueue _papelService;
        private readonly ISendSQSQueue _eventoService;
        private readonly ILogger<RastreamentoMessenger> _logger;

        public RastreamentoEvento EventoEmAndamento { get; set; } = null;
        public List<RastreamentoPapel> RecebidosOnly { get; set; } = new List<RastreamentoPapel>();
        public List<RastreamentoPapel> PersistidosOnly { get; set; } = new List<RastreamentoPapel>();

        public RastreamentoMessenger(IServiceProvider serviceProvider, ILogger<RastreamentoMessenger> logger)
        {
            _logger = logger;
            var allSQSServices = serviceProvider.GetServices<ISendSQSQueue>();
            _papelService = allSQSServices.First(o => o.GetType() == typeof(SendSQSPapel));
            _eventoService = allSQSServices.First(o => o.GetType() == typeof(SendSQSEvento));
        }

        public void SendTrackingMessage(IEnumerable<object> obj2send)
        {
            if(!obj2send.Count().Equals(0))
            {
                //var eventArg = new EventoDetectedEventArgs()
                //{
                //    Message = obj2send
                //};
                if (obj2send.All(obj => typeof(RastreamentoEvento).IsInstanceOfType(obj)))
                {
                    _eventoService.Send(obj2send.First());
                    var message = JsonConvert.SerializeObject(obj2send);
                    _logger.LogInformation($"Mensagem de evento enviada : {message}");
                    //OnEventoDetected(eventArg);
                }
                else if (obj2send.All(obj => typeof(RastreamentoPapel).IsInstanceOfType(obj)))
                {
                    _papelService.SendBatch(obj2send, 100);
                    var message = JsonConvert.SerializeObject(obj2send);
                    _logger.LogInformation($"Mensagem de papel enviada : {message}");
                    //send message                    
                    //OnPapelDetected(eventArg);
                }
                else
                {
                    throw new Exception("O tipo do objeto de mensagem de rastreamento está incorreto");
                }
            }            

        }
        //public void OnEventoDetected(EventoDetectedEventArgs e)
        //{
        //    EventoDetected?.Invoke(this, e);

        //}
        //public void OnPapelDetected(EventoDetectedEventArgs e)
        //{
        //    PapelDetected?.Invoke(this, e);
        //}

        public void FinalizarEventoEmAndamento()
        {
            if (!HouveEnvioMDP && EventoEmAndamento.TipoRequisicao!=TipoRequisicaoEnum.PRECO_HISTORICO.ToString())
            {
                string mensagem = $"Requisição finalizada no BPO: Nenhuma mensagem foi enviada ao MDP";
                if (!HouvePersistenciaBPO)
                {
                    mensagem += "/Nenhum dado foi persistido no BPO";
                }

                if(!HouveFalha)
                {
                    var jsonEvento = JObject.FromObject(new { message = mensagem, erro = "" });
                    var evento = MontarObjetoEvento((TipoRequisicaoEnum)Enum.Parse(typeof(TipoRequisicaoEnum), EventoEmAndamento.TipoRequisicao, true), DateTime.Now, "", StatusProcessamentoEnum.SUCESSO, jsonEvento, "");
                    SendTrackingMessage(evento);
                }                
            }
            EventoEmAndamento.StatusEvento = StatusProcessamentoEnum.SUCESSO.ToString();
            EventoEmAndamento.DataFimEvento = DateTime.Now;
            if (RecebidosOnly.Any())
            {
                var papeis = MontarObjetoPapel(RecebidosOnly, StatusMensagemEnum.RECEBIDO_LUZ, "");
                SendTrackingMessage(papeis);
                RecebidosOnly = new List<RastreamentoPapel>();
            }
            if (PersistidosOnly.Any())
            {                
                var papeis = MontarObjetoPapel(PersistidosOnly, StatusMensagemEnum.PERSISTIDO_BPO, "");
                SendTrackingMessage(papeis);
                PersistidosOnly = new List<RastreamentoPapel>();
            }

            EventoEmAndamento = null;
            HouveEnvioMDP = false;
            HouvePersistenciaBPO = false;
            HouveFalha = false;
        }

        public bool IsEventoIniciado()
        {
            if(EventoEmAndamento==null)
            {
                return false;
            }
            return EventoEmAndamento.StatusEvento == StatusProcessamentoEnum.INICIADO.ToString();
        }

        public IEnumerable<RastreamentoEvento> MontarObjetoEvento(TipoRequisicaoEnum requisicaoTipo, DateTime? dataFimEvento, string metodo, StatusProcessamentoEnum statusEvento, JObject jsonEvento, string? usuario)
        {
            var mensagemEvento = new List<RastreamentoEvento>();
            var mensagem = new RastreamentoEvento();
            mensagem.TipoRequisicao = requisicaoTipo.ToString();
            mensagem.DataFimEvento = dataFimEvento;
            mensagem.Metodo = metodo;
            mensagem.StatusEvento = statusEvento.ToString();
            mensagem.JsonEvento = jsonEvento;
            mensagem.Usuario = usuario;

            if (EventoEmAndamento != null)
            {
                mensagem.DataInicioEvento = EventoEmAndamento.DataInicioEvento;
                mensagem.Usuario = EventoEmAndamento.Usuario;
                mensagem.IdRequisicao = EventoEmAndamento.IdRequisicao;
                mensagem.Metodo = EventoEmAndamento.Metodo;
                mensagem.TipoRequisicao = EventoEmAndamento.TipoRequisicao;
            }
            else
            {
                mensagem.IdRequisicao = Guid.NewGuid();
                mensagem.DataInicioEvento = DateTime.Now;                
            }

            if (statusEvento == StatusProcessamentoEnum.ERRO)
            {
                HouveFalha = true;
            }

            EventoEmAndamento = mensagem;
            mensagemEvento.Add(mensagem);
            return mensagemEvento;
        }

       public IEnumerable<RastreamentoEvento> MontarObjetoEventoFilho(Guid idRequisicao, TipoRequisicaoEnum requisicaoTipo, DateTime? dataFimEvento, string metodo, StatusProcessamentoEnum statusEvento, JObject jsonEvento, string? usuario)
       {
           var mensagemEvento = new List<RastreamentoEvento>();
           var mensagem = new RastreamentoEvento();
           mensagem.TipoRequisicao = requisicaoTipo.ToString();
           mensagem.DataFimEvento = dataFimEvento;
           mensagem.Metodo = metodo;
           mensagem.StatusEvento = statusEvento.ToString();
           mensagem.JsonEvento = jsonEvento;
           mensagem.Usuario = usuario;

           if (EventoEmAndamento != null)
           {
               mensagem.DataInicioEvento = EventoEmAndamento.DataInicioEvento;
               mensagem.Usuario = EventoEmAndamento.Usuario;
               mensagem.IdRequisicao = EventoEmAndamento.IdRequisicao;
               mensagem.Metodo = EventoEmAndamento.Metodo;
               mensagem.TipoRequisicao = EventoEmAndamento.TipoRequisicao;
            }
           else
           {
               mensagem.IdRequisicao = idRequisicao;
               mensagem.DataInicioEvento = DateTime.Now;
           }

            if (statusEvento == StatusProcessamentoEnum.ERRO)
            {
                HouveFalha = true;
            }

            EventoEmAndamento = mensagem;
           mensagemEvento.Add(mensagem);
           return mensagemEvento;
       }

        public IEnumerable<RastreamentoPapel> MontarObjetoPapel(List<RastreamentoPapel> papeisRequest, StatusMensagemEnum statusMensagem, string mensagemErro, bool papelHomologacao = false)
        {
            if (EventoEmAndamento == null)
            {
                throw new Exception("Rastreamento papel foi chamado antes de rastreamento evento");
            }
            foreach (RastreamentoPapel papel in papeisRequest)
            {
                papel.IdRequisicao = EventoEmAndamento.IdRequisicao;
                papel.DataInicioEvento = EventoEmAndamento.DataInicioEvento;
                papel.DataFimEvento = EventoEmAndamento.StatusEvento == StatusProcessamentoEnum.SUCESSO.ToString()? EventoEmAndamento.DataFimEvento : null;
                papel.StatusPapel = EventoEmAndamento.StatusEvento;
                papel.Usuario = EventoEmAndamento.Usuario;
                papel.TipoLog = EventoEmAndamento.TipoRequisicao.ToString();
                papel.StatusMensagem = statusMensagem.ToString();
                papel.MensagemErro = mensagemErro;
                if (papelHomologacao)
                {
                    papel.Papel = "L-" + papel.Papel;
                    if (papel.Papel.Length > 12)
                        papel.Papel = papel.Papel.Substring(0, 12);
                }
            }

            if(statusMensagem == StatusMensagemEnum.PERSISTIDO_BPO && !papeisRequest.Count.Equals(0))
            {
                HouvePersistenciaBPO = true;
            }
            if (statusMensagem == StatusMensagemEnum.ENVIADO_MDP && !papeisRequest.Count.Equals(0))
            {
                HouveEnvioMDP = true;
            }

            return papeisRequest;
        }        
    }
    //public class EventoDetectedEventArgs : EventArgs
    //{
    //    public IEnumerable<object> Message { get; set; }
    //}



}
