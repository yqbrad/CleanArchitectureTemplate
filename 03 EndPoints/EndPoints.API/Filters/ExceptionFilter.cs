using Framework.Domain.Error;
using Framework.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.EndPoints.API.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        //private readonly ILoggerService _loggerService;
        //public ExceptionFilter(ILoggerService loggerService)
        //    => _loggerService = loggerService;

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var statusCode = StatusCodes.Status500InternalServerError;
            var errorCode = 0;
            var message = "خطای سرور";
            //LogType errorType;

#if DEBUG
            message = ex.ToString();
#endif

            switch (ex)
            {
                case BaseException _:
                    statusCode = 499;
                    //errorType = LogType.Warning;
                    message = ex.Message;
                    errorCode = ex.HResult;
                    break;

                case Models.ApiException apiException:
                    statusCode = 499;
                    //errorType = LogType.Warning;
                    message = apiException.Message;
                    errorCode = apiException.HResult;

                    if (apiException.GetResult() is IError error)
                    {
                        errorCode = error.Code;
                        message = error.Message;
                    }
                    break;
                default:
                    //errorType = LogType.Error;
                    break;
            }

            //_loggerService.LogAsync(ex, errorType);

            context.Result = new ObjectResult(new Error(message, errorCode))
            {
                StatusCode = statusCode
            };
        }
    }
}