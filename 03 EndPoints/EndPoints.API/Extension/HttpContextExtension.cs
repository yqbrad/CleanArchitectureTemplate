using DDD.EndPoints.API.Configuration;
using Microsoft.AspNetCore.Http;

namespace DDD.EndPoints.API.Extension
{
    public static class HttpContextExtension
    {
        public static ServiceConfig ServiceContext(this HttpContext httpContext) =>
            (ServiceConfig)httpContext.RequestServices.GetService(typeof(ServiceConfig));
    }
}