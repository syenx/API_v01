using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.Models.SQS;
using System.Collections;

namespace EDM.Infohub.BPO.Processamento
{
    public abstract class AbstractProcessamentoHandler<T> : IProcessamentoHandler<T> where T : IList
    {

        private IProcessamentoHandler<T> _nextHandler;

        public IProcessamentoHandler<T> SetNext(IProcessamentoHandler<T> handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual T Handle(T request, EProcessStep step)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, step);
            }
            else
            {
                return default(T);
            }
        }
    }

    //public static class ProcessamentoFactory
    //{
    //    public static AbstractProcessamentoHandler<T> CreateInstance(EProcessamentoTipo tipo)
    //    {
    //        switch (tipo)
    //        {
    //            case EProcessamentoTipo.Cadastro:
    //                return new CadastroHandler();
    //            case EProcessamentoTipo.Preco:
    //                return new PrecoHandler();
    //            case EProcessamentoTipo.Pu_Evento:
    //                return new PrecoEventoHandler();
    //            case EProcessamentoTipo.Pu_Historico:
    //                return new PrecoHistoricoHandler();
    //            default:
    //                return new HandlerImplementation();
    //        }
    //    }
    //}
}
