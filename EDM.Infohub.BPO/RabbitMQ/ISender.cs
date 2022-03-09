using EDM.Infohub.BPO.Models.SQS;
using System;
using System.Collections.Generic;

namespace config.rabbitMQ
{
    public interface ISender
    {
        void SendObj(string queueName, object obj, bool isDurable);
        void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, int size = 100);
        void SendBulk<T>(string queueName, IEnumerable<T> message, bool isDurable);
        void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, int size = 100);
        void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, bool homologacao, int size = 100);
        void SendLot<T>(string queueName, IEnumerable<T> message, bool isDurable, DateTime data, bool homologacao, RastreamentoEvento eventoEmAndamento, int size = 100);
    }
}
