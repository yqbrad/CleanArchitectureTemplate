using System.Collections.Generic;

namespace Framework.Domain.Error
{
    public interface IError
    {
        public IEnumerable<string> Messages { get; set; }
        int Code { get; set; }
    }
}