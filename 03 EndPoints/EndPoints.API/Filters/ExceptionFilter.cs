using Framework.Domain.Error;
using Helper.Exceptions;
using Helper.Exceptions.Exceptions;
using Logger.EndPoints.Service.Base;
using Logger.EndPoints.Service.Logger.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.EndPoints.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerService _loggerService;
        public ExceptionFilter(ILoggerService loggerService)
            => _loggerService = loggerService;

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var statusCode = StatusCodes.Status500InternalServerError;
            var errorCode = 0;
            var message = "خطای سرور";
            LogType errorType;

            switch (ex)
            {
                case UnauthorizedException _:
                    context.Result = new UnauthorizedObjectResult(new Error(ex.Message, ex.HResult));
                    return;
                case BaseException _:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorType = LogType.Warning;
                    message = ex.Message;
                    errorCode = ex.HResult;
                    break;
                default:
                    errorType = LogType.Error;
                    break;
            }

            _loggerService.LogAsync(ex, errorType);

            context.Result = new ObjectResult(new Error(message, errorCode))
            {
                StatusCode = statusCode
            };
        }
    }
}