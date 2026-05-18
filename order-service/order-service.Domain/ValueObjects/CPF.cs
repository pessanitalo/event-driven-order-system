using order_service.Domain.Validation;


namespace order_service.Domain.ValueObjects
{
    public class CPF
    {
        public string Valor { get; private set; }

        public CPF(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor), "CPF é obrigatório.");

            DomainExceptionValidation.When(valor.Length != 11, "O CPF precisa ter onze caracteres.");

            Valor = valor;
        }

        public static CPF Criar(string valor) => new(valor);
    }
}
