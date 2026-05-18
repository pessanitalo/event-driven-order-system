using order_service.Domain.ValueObjects;
using ItemPedidoDomain = order_service.Domain.ItemPedido.Domain.Entities.ItemPedido;

namespace order_service.Domain.Pedidos.Domain.Entities
{
    public class Pedido
    {
        public Guid PedidoId { get; set; }
        public NomeCliente NomeCliente { get; set; }
        public CPF CpfCliente { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string IdempotencyKey { get; set; }
        public List<ItemPedidoDomain> Itens { get; set; } = new();

        public decimal CalcularTotal()
        {
            return Itens.Sum(item => item.CalcularSubtotal());
        }

        public void AtualizarTotal()
        {
            PrecoTotal = CalcularTotal();
        }

        private Pedido() { }

        public Pedido(Guid pedidoId, NomeCliente nomeCliente, CPF cpfCliente, decimal precoTotal, DateTime createAt, DateTime updateAt, string idempotencyKey)
        {
            PedidoId = pedidoId;
            NomeCliente = nomeCliente;
            CpfCliente = cpfCliente;
            PrecoTotal = precoTotal;
            CreateAt = createAt;
            UpdateAt = updateAt;
            IdempotencyKey = idempotencyKey;
        }

        public static Pedido CriarPedido(Guid pedidoId, NomeCliente nomeCliente, CPF cpfCliente, decimal precoTotal, DateTime createAt, DateTime updateAt, string idempotencyKey)
        {
            return new Pedido(pedidoId, nomeCliente, cpfCliente, precoTotal, createAt, updateAt, idempotencyKey);
        }

        public static Pedido ReconstruirDoBanco(Guid pedidoId, NomeCliente nomeCliente, CPF cpfCliente,
                                          decimal precoTotal, DateTime createAt, DateTime updateAt,
                                          string idempotencyKey)
        {
            return new Pedido(pedidoId, nomeCliente, cpfCliente, precoTotal, createAt, updateAt, idempotencyKey);
        }
    }
}
