using Helper.Exceptions;

namespace $safeprojectname$._Exceptions
{
    public class InvalidValueObjectStateException: BaseException
    {
        public override int ExCode => ExceptionType.InvalidValueObjectStateException.Code();
        public override string ExMessage { get; }

        public InvalidValueObjectStateException(string exMessage)
        {
            ExMessage = exMessage;
        }
    }
}