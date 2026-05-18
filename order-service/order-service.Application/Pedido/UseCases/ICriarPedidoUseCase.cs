using order_service.Application.Pedido.DTOs;
using order_service.Domain.Common;


namespace order_service.Application.Pedido.UseCases
{
    public interface ICriarPedidoUseCase
    {
        Task<Result<string>> CriarPedidoAsync(CriarPedidoRequest criarPedidoRequest);
    }
}
