using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;
using Thunders.TechTest.Infra.Context;

namespace Thunders.TechTest.Infra.Repositories
{
    public class RelatorioTopPracasRepository(ApplicationDbContext context) : Repository<RelatorioTopPracas>(context), IRelatorioTopPracasRepository
    {
    }
}
