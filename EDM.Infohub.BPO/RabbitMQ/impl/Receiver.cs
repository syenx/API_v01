using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace config.rabbitMQ.impl
{
    public class Receiver : IReceiver
    {
        private readonly IRabbitMQConnection rabbitMQConnection;
        private readonly ILogger<Receiver> _logger;
        private readonly IConfiguration _configuration;

        public Receiver(IRabbitMQConnection rabbitMQConnection, ILogger<Receiver> logger, IConfiguration configuration)
        {
            this.rabbitMQConnection = rabbitMQConnection;
            _logger = logger;
            _configuration = configuration;
        }

        public void Connect(string connectionName)
        {
            rabbitMQConnection.ConConnect(connectionName);
        }

        public void Receive(string queueConfiguration, Action<string> func, bool isDurable)
        {
            var channel = rabbitMQConnection.QueueDeclareComplete(queueConfiguration);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                try
                {
                    _logger.LogInformation($"Recebendo na fila '{queueConfiguration}' a mensagem: {message}");

                    func.Invoke(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Erro no recebimento da mensagem. QueueName: {queueConfiguration}. Mensagem: {ea.Body.ToString()}. Erro: {e.Message}");
                    channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    throw;
                }
                finally
                {
                    _logger.LogInformation("Dando ACK");
                }
            };
            channel.BasicQos(0, 5, false);
            channel.BasicConsume(queue: _configuration[$"RabbitMQ:{queueConfiguration}:QueueName"],
                                 autoAck: false,
                                 consumer: consumer);

        }

        public void ReceiveDeadLetter(string queueConfiguration, Action<string> func, bool isDurable)
        {
            var channel = rabbitMQConnection.QueueDeclare(_configuration[$"RabbitMQ:{queueConfiguration}:QueueName"], isDurable);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                try
                {
                    _logger.LogInformation($"Recebendo na fila '{queueConfiguration}' a mensagem: {message}");

                    func.Invoke(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Erro no recebimento da mensagem. QueueName: {queueConfiguration}. Mensagem: {ea.Body.ToString()}. Erro: {e.Message}");
                    channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    throw;
                }
                finally
                {
                    _logger.LogInformation("Dando ACK");
                }
            };
            channel.BasicQos(0, 5, false);
            channel.BasicConsume(queue: _configuration[$"RabbitMQ:{queueConfiguration}:QueueName"],
                                 autoAck: false,
                                 consumer: consumer);

        }
    }
}
