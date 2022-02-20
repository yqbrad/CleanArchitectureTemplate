using System;

namespace $safeprojectname$.Exceptions;

public class DataAccessException : BaseException
{
    public override int ExCode => -1;
    public override string ExMessage => "DatabaseError";

    public DataAccessException(Exception ex) : base(ex) { }
}