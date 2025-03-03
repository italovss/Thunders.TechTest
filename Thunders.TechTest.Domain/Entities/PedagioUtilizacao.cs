namespace Thunders.TechTest.Domain.Entities
{
    public class PedagioUtilizacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataHora { get; set; }
        public string Praca { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal ValorPago { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
    }
}
