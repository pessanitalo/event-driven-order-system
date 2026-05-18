using order_service.Domain.Validation;
namespace order_service.Domain.ValueObjects
{
    public class PrecoTotalPedido
    {
        public decimal Valor { get; set; }

        public PrecoTotalPedido(decimal valor)
        {
            DomainExceptionValidation.When(valor <= 0, "O preço total do pedido deve ser maior que zero.");

            Valor = valor;
        }
    }
}
