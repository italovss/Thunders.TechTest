using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedagioController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IEnumerable<PedagioUtilizacao>> GetAllAsync()
        {
            return await _unitOfWork.PedagioRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PedagioUtilizacao pedagio)
        {
            await _unitOfWork.PedagioRepository.AddAsync(pedagio);
            var sucesso = await _unitOfWork.CommitAsync() > 0;

            if (!sucesso)
                return BadRequest("Erro ao salvar pedagio.");

            return Ok(pedagio);
        }
    }
}
