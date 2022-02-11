using System;
using System.Threading.Tasks;
using Framework.Domain.Logger;

namespace DDD.Infrastructure.Service.Logger
{
    public class LoggerService : ILoggerService
    {
        public Task LogAsync(Exception ex)
        {
            //TODO
            return Task.CompletedTask;
        }
    }
}