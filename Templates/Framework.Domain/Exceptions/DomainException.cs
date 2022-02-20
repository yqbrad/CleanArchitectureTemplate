namespace $safeprojectname$.Exceptions
{
    public class DomainException : BaseException
    {
        public override int ExCode => 1;
        public override string ExMessage { get; }
        public string[] Parameters { get; }

        public DomainException(string exMessage, params string[] parameters)
            : base(exMessage)
        {
            ExMessage = exMessage;
            Parameters = parameters;
        }
    }
}