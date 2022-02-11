using Framework.Domain.Error;
using Framework.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.EndPoints.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
            => _logger = logger;

        public void OnException(ExceptionContext context)
        {
            int statusCode;
            Error error;
            LogLevel logType;

            var ex = context.Exception;
            switch (ex)
            {
                case DomainValidationException e:
                    statusCode = StatusCodes.Status400BadRequest;
                    error = new Error(e.ExMessages, e.ExCode);
                    logType = LogLevel.Warning;
                    break;
                case BaseException e:
                    statusCode = StatusCodes.Status400BadRequest;
                    error = new Error(e.Message, e.ExCode);
                    logType = LogLevel.Warning;
                    break;

                //case Models.ApiException apiException:
                //    statusCode = 499;
                //    //errorType = LogType.Warning;
                //    message = apiException.Message;
                //    errorCode = apiException.HResult;

                //    if (apiException.GetResult() is IError error)
                //    {
                //        errorCode = error.Code;
                //        message = error.Message;
                //    }
                //    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    var defaultMessage = "خطای سرور";
#if DEBUG
                    defaultMessage = ex.ToString();
#endif
                    error = new Error(defaultMessage, 0);
                    logType = LogLevel.Error;
                    break;
            }

            _logger.Log(logType, ex, GetInnermostExceptionMessage(ex), error.Messages);

            context.Result = new ObjectResult(error)
            {
                StatusCode = statusCode
            };
        }

        private static string GetInnermostExceptionMessage(Exception exception)
            => exception.InnerException is not null ?
                GetInnermostExceptionMessage(exception.InnerException) :
                exception.Message;
    }
}