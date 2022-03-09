using System;

namespace config.rabbitMQ
{
    public interface IReceiver
    {
        void Connect(string connectionName);
        void Receive(string queueName, Action<string> func, bool isDurable);
        void ReceiveDeadLetter(string queueName, Action<string> func, bool isDurable);
    }
}
