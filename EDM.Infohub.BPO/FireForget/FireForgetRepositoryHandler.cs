using EDM.Infohub.BPO.Controllers.V2;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO
{
    public class FireForgetRepositoryHandler : IFireForgetRepositoryHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FireForgetRepositoryHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ExecuteFluxos(Func<FluxoController, Task> databaseWork)
        {
            // Fire off the task, but don't await the result
            Task.Run(async () =>
            {
                Console.WriteLine("Entrou no FireForgetGandler");
                // Exceptions must be caught
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<FluxoController>();
                    await databaseWork(repository);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public void ExecuteHistorico(Func<HangfireService, Task> work)
        {
            // Fire off the task, but don't await the result
            Task.Run(async () =>
            {
                Console.WriteLine("Entrou no FireForgetHandler");
                // Exceptions must be caught
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<HangfireService>();
                    await work(repository);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public void ExecuteCadastro(Func<CaracteristicaController, Task> work)
        {
            // Fire off the task, but don't await the result
            Task.Run(async () =>
            {
                Console.WriteLine("Entrou no FireForgetHandler de cadastro");
                // Exceptions must be caught
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<CaracteristicaController>();
                    await work(repository);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}