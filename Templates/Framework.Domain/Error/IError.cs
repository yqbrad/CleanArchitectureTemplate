using System.Collections.Generic;

namespace $safeprojectname$.Error
{
    public interface IError
    {
        public List<string> Messages { get; set; }
        int Code { get; set; }
    }
}