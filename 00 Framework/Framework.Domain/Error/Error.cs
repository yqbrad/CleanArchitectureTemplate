using System.Collections.Generic;

namespace Framework.Domain.Error
{
    public class Error : IError
    {
        public IEnumerable<string> Messages { get; set; }
        public int Code { get; set; }

        public Error() { }

        public Error(string message, int code)
        {
            Messages = new[] { message };
            Code = code;
        }

        public Error(IEnumerable<string> messages, int code)
        {
            Messages = messages;
            Code = code;
        }
    }
}