namespace Thunders.TechTest.Domain.Entities
{
    public class RelatorioFaturamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataProcessamento { get; set; }
        public string Cidade { get; set; }
        public string TotalPorHora { get; set; }
    }
}
