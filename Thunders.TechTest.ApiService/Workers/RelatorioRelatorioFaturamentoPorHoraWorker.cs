using Rebus.Handlers;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Workers
{
    public class RelatorioRelatorioFaturamentoPorHoraWorker(IUnitOfWork unitOfWork, IRelatorioService relatorioService) : IHandleMessages<RelatorioRelatorioFaturamentoPorHoraMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRelatorioService _relatorioService = relatorioService;

        public async Task Handle(RelatorioRelatorioFaturamentoPorHoraMessage message)
        {
            await _relatorioService.GerarRelatorioFaturamentoPorHora(message.Cidade);
        }
    }
}