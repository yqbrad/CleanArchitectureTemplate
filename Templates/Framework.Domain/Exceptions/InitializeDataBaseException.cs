using System;

namespace $safeprojectname$.Exceptions
{
    public class InitializeDataBaseException : BaseException
    {
        private new const string Message = "InitializeDataBaseError";

        public override int ExCode => -2;
        public override string ExMessage => Message;

        public InitializeDataBaseException(Exception ex) : base(Message, ex) { }
    }
}