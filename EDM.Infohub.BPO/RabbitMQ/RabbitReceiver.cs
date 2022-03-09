using config.rabbitMQ;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace EDM.Infohub.BPO.RabbitMQ
{
    public class RabbitReceiver
    {
        private readonly IReceiver _receiver;

        private readonly ILogger<RabbitReceiver> _logger;

        private MessageProcessor _processor;
        private RastreamentoRepository _rastreamento;

        public RabbitReceiver(IReceiver receiver, ILogger<RabbitReceiver> logger, MessageProcessor processor, RastreamentoRepository rastreamentoRepository)
        {
            _receiver = receiver;
            _logger = logger;
            _processor = processor;

            _rastreamento = rastreamentoRepository;

            //_serviceProvider = serviceProvider;
        }

        public void StartRabbitService()
        {
            Action<string> messageTarget;
            messageTarget = TreatIncomingMessage;

            Action<string> deadLetterTarget;
            deadLetterTarget = TreatDeadLetterMessage;

            // var teste = _controller.Get();
            //Console.WriteLine(teste.ToArray().ToString());

            //_rabbitmq.Connect("ConnectionConfiguration", "QueueConfiguration");
            _receiver.Connect("ConnectionConfiguration");
            _receiver.Receive("QueueConfiguration", messageTarget, true);
            //_receiver.ReceiveDeadLetter("DeadLetterConfiguration", deadLetterTarget, true);
        }

        private void TreatDeadLetterMessage(string obj)
        {
            Console.WriteLine(obj);
            var teste = JsonConvert.DeserializeObject<DeadLetter>(obj);
            var dadosCarac = JsonConvert.DeserializeObject<DadosCaracteristicos>(teste.mensagem);
            var teste1 = new RastreamentoModel()
            {
                cd_sna = dadosCarac.CodigoSNA,
                dh_rank = Utils.GenerateRank(DateTime.Now),
                dh_evento = DateTime.Now,
                en_status = StatusMensagemEnum.ERRO_MDP.ToString(),
                en_tipo = TipoLogEnum.CADASTRO.ToString(),
                tx_erro = obj
            };
            _rastreamento.Insert(teste1);
        }

        private void TreatIncomingMessage(string message)
        {
            _logger.LogInformation($"Mensagem lida: {message}");

            var messageObj = JsonConvert.DeserializeObject<InfohubMessageModel>(message);

            _processor.Process(messageObj);

        }

        public class DeadLetter
        {
            [JsonProperty(PropertyName = "mensagem")]
            public string mensagem { get; set; }
            [JsonProperty(PropertyName = "erro")]
            public string erro { get; set; }
        }
    }
}
