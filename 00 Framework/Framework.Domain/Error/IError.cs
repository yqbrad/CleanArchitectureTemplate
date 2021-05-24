namespace Framework.Domain.Error
{
    public interface IError
    {
        string Message { get; set; }
        int Code { get; set; }
    }
}