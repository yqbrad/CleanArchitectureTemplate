using System.Collections.Generic;

namespace Framework.Domain.Error
{
    public interface IError
    {
        public List<string> Messages { get; set; }
        int Code { get; set; }
    }
}