using order_service.Domain.Validation;


namespace order_service.Domain.ValueObjects
{
    public class NomeCliente
    {
        public string Valor { get; set; }

        public NomeCliente(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor),
                "O nome do cliente é obrigatório.");
            DomainExceptionValidation.When(valor.Length < 3,
                "O nome do cliente precisa ter no mínimo três caracteres.");
            Valor = valor;
        }

        public static NomeCliente Criar(string valor) => new(valor);
    }
}
