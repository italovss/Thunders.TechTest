using Rebus.Handlers;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Workers
{
    public class RelatorioTopPracasWorker(IUnitOfWork unitOfWork, IRelatorioService relatorioService) : IHandleMessages<RelatorioTopPracasMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRelatorioService _relatorioService = relatorioService;

        public async Task Handle(RelatorioTopPracasMessage message)
        {
            await _relatorioService.GerarRelatorioTopPracas(message.MesAno, message.Quantidade);
        }
    }
}