using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioVeiculosPorPracaController(IMessageSender messageSender) : ControllerBase
    {
        private readonly IMessageSender _messageSender = messageSender;

        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarRelatorio([FromBody] RelatorioVeiculosPorPracaMessage request)
        {
            try
            {
                await _messageSender.Publish(request);
                return Ok("Processamento iniciado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao iniciar o processamento: {ex.Message}");
            }
        }
    }
}
