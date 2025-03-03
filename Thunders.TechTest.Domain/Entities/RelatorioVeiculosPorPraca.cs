namespace Thunders.TechTest.Domain.Entities
{
    public class RelatorioVeiculosPorPraca
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Praca { get; set; }
        public string QuantidadeVeiculos { get; set; }
    }
}
