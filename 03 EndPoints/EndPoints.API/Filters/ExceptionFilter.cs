using Framework.Domain.Error;
using Framework.Domain.Exceptions;
using Framework.Domain.Translator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YQB.EndPoints.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ITranslator _translator;
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ITranslator translator, ILogger<ExceptionFilter> logger)
        {
            _translator = translator;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Error error;
            var statusCode = 499;
            var logType = LogLevel.Warning;

            var ex = context.Exception;
            switch (ex)
            {
                case FluentValidationException e:
                    error = new Error(e.Messages.ToList(), e.ExCode);
                    break;
                case DomainException e:
                    error = new Error(e.ExCode);
                    error.Messages.Add(e.Parameters.Any()
                        ? _translator[e.Message, e.Parameters]
                        : _translator[e.Message]);
                    break;
                case BaseException e:
                    error = new Error(_translator[e.Message], e.ExCode);
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
                    var defaultMessage = "ServerError";
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
            => exception.InnerException is not null
                ? GetInnermostExceptionMessage(exception.InnerException)
                : exception.Message;
    }
}