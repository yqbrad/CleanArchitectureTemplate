namespace Framework.Domain.Error
{
    public class Error: IError
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public Error(string message, int code)
        {
            Message = message;
            Code = code;
        }
    }
}