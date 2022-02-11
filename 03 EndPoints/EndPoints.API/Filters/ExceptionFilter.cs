using Framework.Domain.Error;
using Framework.Domain.Exceptions;
using Framework.Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.EndPoints.API.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILoggerService _loggerService;
        public ExceptionFilter(ILoggerService loggerService)
            => _loggerService = loggerService;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            int statusCode;
            Error error;

            switch (context.Exception)
            {
                case ValidationRequestException e:
                    statusCode = StatusCodes.Status400BadRequest;
                    error = new Error(e.ExMessages, e.ExCode);
                    break;
                case BaseException e:
                    statusCode = StatusCodes.Status400BadRequest;
                    error = new Error(e.Message, e.ExCode);
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
                    defaultMessage = context.Exception.ToString();
#endif
                    error = new Error(defaultMessage, 0);
                    break;
            }

            await _loggerService.LogAsync(context.Exception);

            context.Result = new ObjectResult(error)
            {
                StatusCode = statusCode
            };
        }
    }
}