namespace Framework.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        public override int ExCode => 2;
        public override string ExMessage { get; }

        public DomainException(string message)
        {
            ExMessage = message;
        }
    }
}