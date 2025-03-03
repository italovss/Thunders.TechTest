using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.ApiService.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task GerarRelatorioFaturamentoPorHora(string cidade);
        Task GerarRelatorioTopPracas(DateTime mesAno, int quantidade);
        Task GerarRelatorioVeiculosPorPraca(string praca);
    }
}
