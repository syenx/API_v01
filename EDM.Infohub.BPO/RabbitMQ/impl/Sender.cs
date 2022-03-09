using EDM.Infohub.BPO.Models.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace config.rabbitMQ.impl
{
    public class Sender : ISender
    {
        private readonly IConfiguration configuration;
        private readonly IRabbitMQConnection _rabbitMQConnection;
        ILogger<Sender> _logger;
        private static string codPraca = "";
        private static int codFeeder = 17;

        public Sender(IConfiguration configuration, IRabbitMQConnection rabbitMQConnection, ILogger<Sender> logger)
        {
            this.configuration = configuration;
            codPraca = this.configuration["CodigoPraca"];
            codFeeder = Int32.Parse(this.configuration["CodigoFeeder"]);
            _logger = logger;
            _rabbitMQConnection = rabbitMQConnection;
        }

        public void SendObj(string queueName, object obj, bool isDurable)
        {
            //(var connection, var channel) = rabbitMQConnection.Connect(connectionName, queueName);
            var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);
            try
            {
                Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], obj, isDurable);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. Nao foi possivel converter o objeto em JSON. QueueName: {queueName}. Objeto: {obj.ToString()}. Erro: {e.Message}");
                throw;
            }
            //finally
            //{
            //    CloseConnection(connection, channel);
            //}
        }

        public void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, int size = 100)
        {
            var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);

            try
            {
                var enumeration = message.Select((item, index) => new { index, item })
                    .GroupBy(obj => obj.index / size);
                foreach (var items in enumeration)
                {
                    var loteItems = items.Select(obj => obj.item);
                    try
                    {
                        Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], loteItems, isDurable);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Erro no envio de um lote para a fila {configuration[$"RabbitMQ:{queueName}:QueueName"]}. Message: '{JsonConvert.SerializeObject(loteItems)}'. Erro: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio de mensagens em lote para a fila {configuration[$"RabbitMQ:{queueName}:QueueName"]}. Messages: {JsonConvert.SerializeObject(message)}. Erro: {e.Message}");
                throw;
            }
            finally
            {
                CloseChannel(channel);
            }
        }

        public void SendBulk<T>(string queueName, IEnumerable<T> message, bool isDurable)
        {
            var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);

            try
            {
                foreach (var items in message)
                {
                    try
                    {
                        Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], items, isDurable);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Erro no envio de uma mensagem. Message: '{JsonConvert.SerializeObject(items)}'. Erro: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio das mensagens. Messages: {JsonConvert.SerializeObject(message)}. Erro: {e.Message}");
                throw;
            }
            finally
            {
                CloseChannel(channel);
            }
        }

        private void CloseConnection(IConnection connection, IModel channel)
        {
            channel.Close();
            connection.Close();
        }

        private void CloseChannel(IModel channel)
        {
            channel.Close();
        }

        private void Send(IModel channel, string queueName, object obj, bool isDurable, DateTime data)
        {
            string message;
            try
            {
                message = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. Nao foi possivel converter o objeto em JSON. QueueName: {queueName}. Objeto: {obj.ToString()}. Erro: {e.Message}");
                throw;
            }
            try
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("COD_PRACA", codPraca);
                properties.Headers.Add("COD_FEEDER", codFeeder);
                properties.Headers.Add("DATA_ATUALIZACAO", data.ToString("yyyy-MM-dd"));
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. QueueName: {queueName}. Mensagem: {message}. Erro: {e.Message}");
                throw;
            }
        }

        private void Send(IModel channel, string queueName, object obj, bool isDurable)
        {
            string message;
            try
            {
                message = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. Nao foi possivel converter o objeto em JSON. QueueName: {queueName}. Objeto: {obj.ToString()}. Erro: {e.Message}");
                throw;
            }
            try
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("COD_PRACA", codPraca);
                properties.Headers.Add("COD_FEEDER", codFeeder);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. QueueName: {queueName}. Mensagem: {message}. Erro: {e.Message}");
                throw;
            }
        }

        private void Send(IModel channel, string queueName, object obj, bool isDurable, DateTime data, bool homologacao)
        {
            string message;
            try
            {
                message = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. Nao foi possivel converter o objeto em JSON. QueueName: {queueName}. Objeto: {obj.ToString()}. Erro: {e.Message}");
                throw;
            }
            try
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("COD_PRACA", codPraca);
                properties.Headers.Add("COD_FEEDER", codFeeder);
                properties.Headers.Add("DATA_ATUALIZACAO", data.ToString("yyyy-MM-dd"));
                properties.Headers.Add("HOMOLOGACAO", homologacao.ToString());
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. QueueName: {queueName}. Mensagem: {message}. Erro: {e.Message}");
                throw;
            }
        }


        public void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, int size = 100)
        {
            {
                var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);

                try
                {
                    var enumeration = message.Select((item, index) => new { index, item })
                        .GroupBy(obj => obj.index / size);
                    foreach (var items in enumeration)
                    {
                        var loteItems = items.Select(obj => obj.item);
                        try
                        {
                            Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], loteItems, isDurable, data);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, $"Erro no envio de um lote. Message: '{JsonConvert.SerializeObject(loteItems)}'. Erro: {e.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Erro no envio de mensagens em lote. Messages: {JsonConvert.SerializeObject(message)}. Erro: {e.Message}");
                    throw;
                }
                finally
                {
                    CloseChannel(channel);
                }
            }
        }

        public void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, bool homologacao, int size = 100)
        {
            {
                var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);

                try
                {
                    var enumeration = message.Select((item, index) => new { index, item })
                        .GroupBy(obj => obj.index / size);
                    foreach (var items in enumeration)
                    {
                        var loteItems = items.Select(obj => obj.item);
                        try
                        {
                            Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], loteItems, isDurable, data, homologacao);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, $"Erro no envio de um lote. Message: '{JsonConvert.SerializeObject(loteItems)}'. Erro: {e.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Erro no envio de mensagens em lote. Messages: {JsonConvert.SerializeObject(message)}. Erro: {e.Message}");
                    throw;
                }
                finally
                {
                    CloseChannel(channel);
                }
            }
        }

        public void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, bool homologacao, RastreamentoEvento eventoEmAndamento, int size = 100)
        {
            {
                var channel = _rabbitMQConnection.QueueDeclare(configuration[$"RabbitMQ:{queueName}:QueueName"], isDurable);

                try
                {
                    var enumeration = message.Select((item, index) => new { index, item })
                        .GroupBy(obj => obj.index / size);
                    foreach (var items in enumeration)
                    {
                        var loteItems = items.Select(obj => obj.item);
                        try
                        {
                            Send(channel, configuration[$"RabbitMQ:{queueName}:QueueName"], loteItems, isDurable, data, homologacao, eventoEmAndamento);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, $"Erro no envio de um lote. Message: '{JsonConvert.SerializeObject(loteItems)}'. Erro: {e.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Erro no envio de mensagens em lote. Messages: {JsonConvert.SerializeObject(message)}. Erro: {e.Message}");
                    throw;
                }
                finally
                {
                    CloseChannel(channel);
                }
            }
        }
        private void Send(IModel channel, string queueName, object obj, bool isDurable, DateTime data, bool homologacao, RastreamentoEvento eventoEmAndamento)
        {
            string message;
            try
            {
                message = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. Nao foi possivel converter o objeto em JSON. QueueName: {queueName}. Objeto: {obj.ToString()}. Erro: {e.Message}");
                throw;
            }
            try
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("COD_PRACA", codPraca);
                properties.Headers.Add("COD_FEEDER", codFeeder);
                properties.Headers.Add("DATA_ATUALIZACAO", data.ToString("yyyy-MM-dd"));
                properties.Headers.Add("HOMOLOGACAO", homologacao.ToString());
                properties.Headers.Add("EVENTO_ID", eventoEmAndamento.IdRequisicao.ToByteArray());
                properties.Headers.Add("EVENTO_DATAINICIO", JsonConvert.SerializeObject(eventoEmAndamento.DataInicioEvento));
                properties.Headers.Add("EVENTO_USER", eventoEmAndamento.Usuario);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no envio da mensagem. QueueName: {queueName}. Mensagem: {message}. Erro: {e.Message}");
                throw;
            }
        }
    }
}
