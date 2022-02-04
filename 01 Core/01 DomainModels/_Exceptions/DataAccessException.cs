using System;

namespace DDD.DomainModels._Exceptions;

public class DataAccessException : BaseException
{
    public override int ExCode => 1;
    public override string ExMessage => "Data base error.";

    public DataAccessException(Exception ex) : base(ex) { }
}