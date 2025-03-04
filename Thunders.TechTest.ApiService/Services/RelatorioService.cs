using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Services
{
    public class RelatorioService(IUnitOfWork unitOfWork) : IRelatorioService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task GerarRelatorioFaturamentoPorHora(string cidade)
        {
            var resultado = await _unitOfWork.PedagioRepository.Query()
                .Where(p => p.Cidade == cidade)
                .GroupBy(p => new { p.Cidade, p.DataHora.Hour })
                .Select(g => new
                {
                    g.Key.Cidade,
                    Hora = g.Key.Hour,
                    TotalFaturado = g.Sum(x => x.ValorPago)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            if (resultado.Count == 0)
                return;

            var relatorio = new RelatorioFaturamento
            {
                Id = Guid.NewGuid(),
                DataProcessamento = DateTime.UtcNow,
                Cidade = cidade,
                TotalPorHora = System.Text.Json.JsonSerializer.Serialize(resultado)
            };

            await _unitOfWork.RelatorioFaturamentoRepository.AddAsync(relatorio);
            await _unitOfWork.CommitAsync();
        }

        public async Task GerarRelatorioTopPracas(DateTime mesAno, int quantidade)
        {
            var resultado = await _unitOfWork.PedagioRepository.Query()
                .Where(p => p.DataHora.Year == mesAno.Year && p.DataHora.Month == mesAno.Month)
                .GroupBy(p => p.Praca)
                .Select(g => new
                {
                    Praca = g.Key,
                    TotalFaturado = g.Sum(x => x.ValorPago)
                })
                .OrderByDescending(r => r.TotalFaturado)
                .Take(quantidade)
                .ToListAsync()
                .ConfigureAwait(false);

            if (resultado.Count == 0)
                return;

            var relatorio = new RelatorioTopPracas
            {
                Id = Guid.NewGuid(),
                MesAno = mesAno,
                TopPracas = System.Text.Json.JsonSerializer.Serialize(resultado)
            };

            await _unitOfWork.RelatorioTopPracasRepository.AddAsync(relatorio);
            await _unitOfWork.CommitAsync();
        }

        public async Task GerarRelatorioVeiculosPorPraca(string praca)
        {
            var resultado = await _unitOfWork.PedagioRepository.Query()
                .Where(p => p.Praca == praca)
                .GroupBy(p => p.Praca)
                .Select(g => new
                {
                    Praca = g.Key,
                    QuantidadeVeiculos = g.Count()
                })
                .ToListAsync()
                .ConfigureAwait(false);

            if (resultado.Count == 0)
                return;

            var relatorio = new RelatorioVeiculosPorPraca
            {
                Id = Guid.NewGuid(),
                Praca = praca,
                QuantidadeVeiculos = System.Text.Json.JsonSerializer.Serialize(resultado)
            };

            await _unitOfWork.RelatorioVeiculosPorPracaRepository.AddAsync(relatorio);
            await _unitOfWork.CommitAsync();
        }
    }
}
