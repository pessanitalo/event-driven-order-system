namespace order_service.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public List<string> Errors { get; }

        public DomainExceptionValidation(string error)
            : base(error)
        {
            Errors = new List<string> { error };
        }

        public DomainExceptionValidation(List<string> errors)
            : base(string.Join(" | ", errors))
        {
            Errors = errors;
        }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
