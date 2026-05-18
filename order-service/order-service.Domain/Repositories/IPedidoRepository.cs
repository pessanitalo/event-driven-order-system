using order_service.Domain.Pedidos.Domain.Entities;

namespace order_service.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task CreatePedidoAsync(Pedido pedido);
        Task<Pedido> GetByIdempotencyKey(string key);
    }
}
