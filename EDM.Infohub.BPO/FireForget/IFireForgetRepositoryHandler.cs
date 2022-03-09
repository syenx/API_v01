using EDM.Infohub.BPO.Services.impl;
using System;
using System.Threading.Tasks;
using FluxoController = EDM.Infohub.BPO.Controllers.V2.FluxoController;
using CaracteristicaController = EDM.Infohub.BPO.Controllers.V2.CaracteristicaController;

namespace EDM.Infohub.BPO
{
    public interface IFireForgetRepositoryHandler
    {
        //void Execute(Func<IRepository, Task> databaseWork);

        void ExecuteCadastro(Func<CaracteristicaController, Task> work);

        void ExecuteFluxos(Func<FluxoController, Task> databaseWork);

        void ExecuteHistorico(Func<HangfireService, Task> work);
    }
}