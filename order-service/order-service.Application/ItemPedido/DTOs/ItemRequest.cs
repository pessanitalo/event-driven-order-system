

namespace order_service.Application.ItemPedido.DTOs
{
    public class ItemRequest
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
