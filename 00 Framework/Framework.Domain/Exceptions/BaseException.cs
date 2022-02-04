using System;

namespace Framework.Domain.Exceptions;

[Serializable]
public abstract class BaseException : ApplicationException
{
    public abstract int ExCode { get; }
    public abstract string ExMessage { get; }

    internal BaseException() { }

    internal BaseException(Exception ex) : base("", ex) { }

    internal BaseException(string message) : base(message) { }
}