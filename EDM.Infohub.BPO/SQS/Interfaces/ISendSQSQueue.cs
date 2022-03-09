using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.SQS.Interfaces
{
    public interface ISendSQSQueue
    {
        public void Send(object instance);
        public void SendBatch(IEnumerable<object> instances, int batchSize);
    }
}
