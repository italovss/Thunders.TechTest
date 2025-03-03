namespace Thunders.TechTest.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPedagioRepository PedagioRepository { get; }
        Task<int> CommitAsync();
    }
}
