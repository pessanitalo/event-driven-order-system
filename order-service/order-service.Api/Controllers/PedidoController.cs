using Microsoft.AspNetCore.Mvc;
using order_service.Application.Pedido.DTOs;
using order_service.Application.Pedido.UseCases;

namespace order_service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly ILogger<PedidoController> _logger;
        private readonly ICriarPedidoUseCase _criarPedidoUseCase;

        public PedidoController(ILogger<PedidoController> logger, ICriarPedidoUseCase criarPedidoUseCase)
        {
            _logger = logger;
            _criarPedidoUseCase = criarPedidoUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest pedidoDto)
        {
            var pedido = await _criarPedidoUseCase.CriarPedidoAsync(pedidoDto);
            if (!pedido.Success)
                return BadRequest(new { errors = new[] { pedido.Error } });
            return Created(string.Empty, new { data = pedido.Data });
        }
    }
}
