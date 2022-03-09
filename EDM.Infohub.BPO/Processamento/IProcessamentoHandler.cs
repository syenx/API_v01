using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models.SQS;
using System.Collections;

namespace EDM.Infohub.BPO.Processamento
{
    public interface IProcessamentoHandler<T> where T : IList
    {
        IProcessamentoHandler<T> SetNext(IProcessamentoHandler<T> handler);

        T Handle(T request, EProcessStep step);
    }

    public enum EProcessStep
    {
        DataBase = 1,
        MDP = 2
    }
}