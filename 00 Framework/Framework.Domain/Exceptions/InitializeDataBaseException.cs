using System;

namespace Framework.Domain.Exceptions
{
    public class InitializeDataBaseException:BaseException
    {
        public override int ExCode => -2;
        public override string ExMessage => "خطای آماده سازی اولیه دیتابیس.";

        public InitializeDataBaseException(Exception ex) : base(ex) { }
    }
}