using Rebus.Handlers;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Workers
{
    public class RelatorioVeiculosPorPracaWorker(IUnitOfWork unitOfWork, IRelatorioService relatorioService) : IHandleMessages<RelatorioVeiculosPorPracaMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRelatorioService _relatorioService = relatorioService;

        public async Task Handle(RelatorioVeiculosPorPracaMessage message)
        {
            await _relatorioService.GerarRelatorioVeiculosPorPraca(message.Praca);
        }
    }
}