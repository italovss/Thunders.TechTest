using Thunders.TechTest.Domain.Interfaces;
using Thunders.TechTest.Infra.Context;

namespace Thunders.TechTest.Infra.Repositories
{
    public class UnitOfWork(ApplicationDbContext context, IPedagioRepository pedagioRepository) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPedagioRepository _pedagioRepository = pedagioRepository;

        public IPedagioRepository PedagioRepository => _pedagioRepository;

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
