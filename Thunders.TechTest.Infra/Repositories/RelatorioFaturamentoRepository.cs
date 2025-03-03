using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;
using Thunders.TechTest.Infra.Context;

namespace Thunders.TechTest.Infra.Repositories
{
    public class RelatorioFaturamentoRepository(ApplicationDbContext context) : Repository<RelatorioFaturamento>(context), IRelatorioFaturamentoRepository
    {
    }
}
