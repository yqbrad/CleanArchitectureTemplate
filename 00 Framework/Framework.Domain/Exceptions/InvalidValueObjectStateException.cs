namespace Framework.Domain.Exceptions
{
    public class InvalidValueObjectStateException : BaseException
    {
        public override int ExCode => 1;
        public override string ExMessage { get; }

        public InvalidValueObjectStateException(string exMessage)
        {
            ExMessage = exMessage;
        }
    }
}