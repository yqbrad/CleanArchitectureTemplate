namespace DDD.DomainModels._Exceptions
{
    public class DomainException : BaseException
    {
        public override int ExCode => 0;
        public override string ExMessage { get; }

        public DomainException(string message)
        {
            ExMessage = message;
        }
    }
}