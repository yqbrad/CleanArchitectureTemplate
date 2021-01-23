namespace Framework.Domain.Error
{
    public class Error
    {
        public Error(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; set; }
        public int Code { get; set; }
    }
}