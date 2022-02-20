namespace $safeprojectname$.Models
{
    public class ApiException: Exception
    {
        public ApiException(
            string message,
            int statusCode,
            string response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers,
            Exception innerException)
            : base(
                $"{message} (Status: {statusCode}, Response: {(response == null ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length))})",
                innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public override string ToString() => Response;

        public virtual object GetResult() => default;
    }

    public class ApiException<TResult>: ApiException
    {
        public ApiException(
            string message,
            int statusCode,
            string response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers,
            TResult result,
            Exception innerException)
            : base(message, statusCode, response, headers, innerException) => Result = result;

        public TResult Result { get; private set; }

        public override object GetResult() => Result;
    }
}