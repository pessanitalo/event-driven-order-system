namespace order_service.Application.Pedido.DTOs
{
    public class PedidoCriadoEvent
    {
        public Guid PedidoId { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public string IdempotencyKey { get; set; } 
    }
}
