using System.Collections.Generic;

namespace Framework.Domain.Error
{
    public class Error : IError
    {
        public List<string> Messages { get; set; }
        public int Code { get; set; }

        public Error() { }

        public Error(int code)
        {
            Messages = new List<string>();
            Code = code;
        }

        public Error(string message, int code)
        {
            Messages = new List<string> { message };
            Code = code;
        }

        public Error(List<string> messages, int code)
        {
            Messages = messages;
            Code = code;
        }
    }
}