using order_service.Domain.Validation;

namespace order_service.Domain.ValueObjects
{
    public class NomeProduto
    {
        public string Valor { get; set; }

        public NomeProduto(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor),
                "O nome do produto é obrigatório.");
            DomainExceptionValidation.When(valor.Length < 2,
                "O nome do produto precisa ter no minimo dois caracteres.");
            Valor = valor;
        }
    }
}
