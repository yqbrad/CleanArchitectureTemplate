using System.Collections.Generic;

namespace Framework.Domain.Exceptions
{
    public class DomainValidationException : BaseException
    {
        private new const string Message = "خطا در اعتبارسنجی";

        public override int ExCode => 1;
        public override string ExMessage { get; }
        public IEnumerable<string> ExMessages { get; }

        public DomainValidationException(string exMessage) : base(exMessage)
        {
            ExMessage = exMessage;
        }

        public DomainValidationException(IEnumerable<string> exMessages) : base(Message)
        {
            ExMessage = Message;
            ExMessages = exMessages;
        }
    }
}