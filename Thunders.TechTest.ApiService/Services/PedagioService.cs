using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Services
{
    public class PedagioService(IUnitOfWork unitOfWork) : IPedagioService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task PersistirNoBancoAsync(PedagioUtilizacao request)
        {
            await _unitOfWork.PedagioRepository.AddAsync(request);
            await _unitOfWork.CommitAsync();
        }

        public bool ValidarDados(PedagioUtilizacao request)
        {
            return request != null &&
                   !string.IsNullOrWhiteSpace(request.Praca) &&
                   !string.IsNullOrWhiteSpace(request.Cidade) &&
                   !string.IsNullOrWhiteSpace(request.Estado) &&
                   request.ValorPago > 0 &&
                   Enum.IsDefined(typeof(TipoVeiculo), request.TipoVeiculo);
        }
    }
}
