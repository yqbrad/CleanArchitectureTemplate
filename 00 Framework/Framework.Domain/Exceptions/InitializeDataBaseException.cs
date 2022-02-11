using System;

namespace Framework.Domain.Exceptions
{
    public class InitializeDataBaseException : BaseException
    {
        private new const string Message = "خطای آماده سازی اولیه دیتابیس.";

        public override int ExCode => -2;
        public override string ExMessage => Message;

        public InitializeDataBaseException(Exception ex) : base(Message, ex) { }
    }
}