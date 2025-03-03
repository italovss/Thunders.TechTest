using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.ApiService.Services.Interfaces
{
    public interface IPedagioService
    {
        bool ValidarDados(PedagioUtilizacao request);
        Task PersistirNoBancoAsync(PedagioUtilizacao request);
    }
}
