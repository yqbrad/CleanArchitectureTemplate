using System;

namespace DDD.DomainModels._Exceptions
{
    public class InitializeDataBaseException:BaseException
    {
        public override int ExCode => 1;
        public override string ExMessage => "Initialize DataBase Error.";

        public InitializeDataBaseException(Exception ex) : base(ex) { }
    }
}