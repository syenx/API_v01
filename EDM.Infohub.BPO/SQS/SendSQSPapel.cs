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
    public class SendSQSPapel : ISendSQSQueue
    {
        private readonly ILogger<SendSQSPapel> _logger;
        private readonly SQSSender _papelSender;
        private readonly ISQSQueueConfiguration _SQSPapelQueueConfig;

        public SendSQSPapel(IAmazonSQS sqsClient, ILogger<SendSQSPapel> logger, ISQSConfiguration optionsSQSConfig)//, RastreamentoMessenger rastreamentoMessenger)
        {
            _papelSender = new SQSSender(sqsClient);
            _logger = logger;
            _SQSPapelQueueConfig = optionsSQSConfig.QueuesConfig["papel"];
            //rastreamentoMessenger.PapelDetected += TriggerSenderEvent;
        }

        //private void TriggerSenderEvent(object sender, EventoDetectedEventArgs e)
        //{
        //    var message = JsonConvert.SerializeObject(e.Message);
        //    _logger.LogInformation($"Mensagem de papel enviada : {message}");
        //    //send message
        //    SendBatch(e.Message,100);
        //}

        public async void Send(object papel)
        {
            try
            {
                await _papelSender.Send(papel, _SQSPapelQueueConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro Send SQS Papel :", ex.Message);
                throw ex;
            }
        }

        public async void SendBatch(IEnumerable<object> papeis, int batchSize)
        {
            try
            {
                await _papelSender.SendBatch(papeis, _SQSPapelQueueConfig, batchSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro Send Batch SQS Papel :", ex.Message);
                throw ex;
            }
        }

    }
}
