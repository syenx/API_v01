using Amazon.SQS;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.GenericStructureSQS.SQS;
using EDM.Infohub.BPO.GenericStructureSQS.SQS.Interfaces;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.SQS.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.SQS
{
    public class SendSQSEvento : ISendSQSQueue
    {
        private readonly ILogger<SendSQSEvento> _logger;
        private readonly SQSSender _eventoSender;
        private readonly ISQSQueueConfiguration _SQSEventoQueueConfig;

        public SendSQSEvento(IAmazonSQS sqsClient, ILogger<SendSQSEvento> logger, ISQSConfiguration optionsSQSConfig)//, RastreamentoMessenger rastreamentoMessenger)
        {
            _eventoSender = new SQSSender(sqsClient);
            _logger = logger;
            _SQSEventoQueueConfig = optionsSQSConfig.QueuesConfig["evento"];
            //rastreamentoMessenger.EventoDetected += TriggerSenderEvent;
        }

        //private void TriggerSenderEvent(object sender, EventoDetectedEventArgs e)
        //{            
        //    var message = JsonConvert.SerializeObject(e.Message);
        //    _logger.LogInformation($"Mensagem de evento enviada : {message}");
        //    //send message
        //    Send(e.Message.First());
        //}

        public async void Send(object evento)
        {
            try
            {
                await _eventoSender.Send(evento, _SQSEventoQueueConfig);
                //_logger.LogInformation($"Resultado do envio do papel : {sendResult}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro Send SQS Evento :", ex.Message);
                throw ex;
            }
        }

        public void SendBatch(IEnumerable<object> instances, int batchSize)
        {
            throw new NotImplementedException();
        }

    }
}
