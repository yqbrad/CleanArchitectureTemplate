using System.Collections.Generic;

namespace Framework.Domain.Exceptions
{
    public class ValidationRequestException : BaseException
    {
        public override int ExCode => 3;
        public override string ExMessage { get; }
        public IEnumerable<string> ExMessages { get; }

        public ValidationRequestException(string exMessage)
        {
            ExMessage = exMessage;
        }
        
        public ValidationRequestException(IEnumerable<string> exMessages)
        {
            ExMessages = exMessages;
        }
    }
}