using System;
using System.Threading.Tasks;

namespace Framework.Domain.Logger
{
    public interface ILoggerService
    {
        Task LogAsync(Exception ex);
    }
}