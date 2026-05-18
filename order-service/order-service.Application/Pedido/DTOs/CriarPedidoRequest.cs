using order_service.Application.ItemPedido.DTOs;

namespace order_service.Application.Pedido.DTOs
{
    public class CriarPedidoRequest
    {
        public Guid ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string IdempotencyKey { get; set; }
        public List<ItemRequest> Itens { get; set; }
    }
}
