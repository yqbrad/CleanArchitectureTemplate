using System.Collections.Generic;

namespace Framework.Domain.Exceptions;

public class FluentValidationException : BaseException
{
    private new const string Message = "خطا در اعتبارسنجی";

    public override int ExCode => 3;
    public override string ExMessage { get; }
    public IEnumerable<string> Messages { get; }

    public FluentValidationException(IEnumerable<string> messages) : base(Message)
    {
        ExMessage = Message;
        Messages = messages;
    }
}