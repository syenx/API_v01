using Amazon.SQS;
using Amazon.SQS.Model;
using EDM.Infohub.BPO.GenericStructureSQS.SQS.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.GenericStructureSQS.SQS
{
    public class SQSReceiver : ISQSReceiver
    {
        private readonly IAmazonSQS _sqsClient;

        public SQSReceiver(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient;
        }

        public async Task Receive<T>(Action<string> func, T queueConfigObj) where T : ISQSQueueConfiguration
        {
            try
            {
                var urlQueue = queueConfigObj.QueueURL;
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    QueueUrl = urlQueue,
                    MaxNumberOfMessages = queueConfigObj.MaxNumberOfMessages,
                    WaitTimeSeconds = queueConfigObj.WaitTimeSeconds
                };

                var sqsServerResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
                if (sqsServerResponse.Messages.Any())
                {
                    foreach (Message message in sqsServerResponse.Messages)
                    {
                        func.Invoke(message.Body);
                        await _sqsClient.DeleteMessageAsync(urlQueue, message.ReceiptHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
