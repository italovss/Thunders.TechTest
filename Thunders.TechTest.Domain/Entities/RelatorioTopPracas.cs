namespace Thunders.TechTest.Domain.Entities
{
    public class RelatorioTopPracas
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime MesAno { get; set; }
        public string TopPracas { get; set; }
    }
}
