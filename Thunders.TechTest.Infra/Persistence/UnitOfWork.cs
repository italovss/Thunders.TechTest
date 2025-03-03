using Thunders.TechTest.Domain.Interfaces;
using Thunders.TechTest.Infra.Context;

namespace Thunders.TechTest.Infra.Persistence
{
    public class UnitOfWork(ApplicationDbContext context,
                            IPedagioRepository pedagioRepository,
                            IRelatorioFaturamentoRepository relatorioFaturamentoRepository,
                            IRelatorioTopPracasRepository relatorioTopPracasRepository,
                            IRelatorioVeiculosPorPracaRepository relatorioVeiculosPorPracaRepository) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPedagioRepository _pedagioRepository = pedagioRepository;
        private readonly IRelatorioFaturamentoRepository _relatorioFaturamentoRepository = relatorioFaturamentoRepository;
        private readonly IRelatorioTopPracasRepository _relatorioTopPracasRepository = relatorioTopPracasRepository;
        private readonly IRelatorioVeiculosPorPracaRepository _relatorioVeiculosPorPracaRepository = relatorioVeiculosPorPracaRepository;

        public IPedagioRepository PedagioRepository => _pedagioRepository;
        public IRelatorioFaturamentoRepository RelatorioFaturamentoRepository => _relatorioFaturamentoRepository;
        public IRelatorioTopPracasRepository RelatorioTopPracasRepository => _relatorioTopPracasRepository;
        public IRelatorioVeiculosPorPracaRepository RelatorioVeiculosPorPracaRepository => _relatorioVeiculosPorPracaRepository;

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
