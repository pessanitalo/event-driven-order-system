using order_service.Domain.ValueObjects;

namespace order_service.Domain.ItemPedido.Domain.Entities
{
    public class ItemPedido
    {
        public NomeProduto Produto { get; set; }
        public QuantidadeItem Quantidade { get; set; }
        public ValorUnitarioItem PrecoUnitario { get; set; }

        public ItemPedido() { }

        public ItemPedido(string produto, int quantidade, decimal precoUnitario)
        {
            Produto = new NomeProduto(produto);
            Quantidade = new QuantidadeItem(quantidade);
            PrecoUnitario = new ValorUnitarioItem(precoUnitario);
        }

        public decimal CalcularSubtotal()
        {
            return PrecoUnitario.Valor * Quantidade.Valor;
        }
    }
}
