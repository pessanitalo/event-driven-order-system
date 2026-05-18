using order_service.Domain.Validation;

namespace order_service.Domain.ValueObjects
{
    public class QuantidadeItem
    {
        public int Valor { get; set; }

        public QuantidadeItem(int valor)
        {
            DomainExceptionValidation.When(valor <= 0, "A quantidade do item deve ser maior que zero.");
            Valor = valor;
        }
    }
}
