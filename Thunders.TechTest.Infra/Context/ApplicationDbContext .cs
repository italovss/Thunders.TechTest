using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PedagioUtilizacao> PedagioUtilizacoes { get; set; }
        public DbSet<RelatorioFaturamento> RelatoriosFaturamento { get; set; }
        public DbSet<RelatorioTopPracas> RelatoriosTopPracas { get; set; }
        public DbSet<RelatorioVeiculosPorPraca> RelatoriosVeiculosPorPraca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
