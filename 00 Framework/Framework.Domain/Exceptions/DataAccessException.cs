using System;

namespace Framework.Domain.Exceptions;

public class DataAccessException : BaseException
{
    public override int ExCode => -1;
    public override string ExMessage => "خطای دیتابیس.";

    public DataAccessException(Exception ex) : base(ex) { }
}