using order_service.Domain.Validation;

namespace order_service.Domain.ValueObjects
{
    public class ValorUnitarioItem
    {
        public decimal Valor { get; set; }

        public ValorUnitarioItem(decimal valor)
        {
            DomainExceptionValidation.When(valor <= 0, "O preço do item deve ser maior que zero.");
            Valor = valor;
        }
    }
}
