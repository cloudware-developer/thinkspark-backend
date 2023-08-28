namespace ThinkSpark.Shared.Infrastructure.Exceptions
{
    public class CommercialException : Exception
    {
        public CommercialException()
        {
        }

        public CommercialException(string message) : base(message)
        {
        }

        public CommercialException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
