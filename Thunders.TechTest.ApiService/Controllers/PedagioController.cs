using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedagioController(IPedagioService pedagioService, IMessageSender messageSender) : ControllerBase
    {
        private readonly IPedagioService _pedagioService = pedagioService;
        private readonly IMessageSender _messageSender = messageSender;

        [HttpPost]
        public async Task<IActionResult> RegistrarUtilizacao([FromBody] PedagioUtilizacao request)
        {
            if (request == null)
                return BadRequest("Dados inválidos.");

            if (!_pedagioService.ValidarDados(request))
                return BadRequest("Dados inconsistentes ou inválidos.");

            try
            {
                await _pedagioService.PersistirNoBancoAsync(request);
                await _messageSender.Publish(request);
                return Ok("Registro processado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
            }
        }
    }
}
