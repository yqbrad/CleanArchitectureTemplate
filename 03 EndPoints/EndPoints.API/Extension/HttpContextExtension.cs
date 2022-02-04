using DDD.Infrastructure.Service.Configuration;

namespace DDD.EndPoints.API.Extension
{
    public static class HttpContextExtension
    {
        public static ServiceConfig ServiceContext(this HttpContext httpContext) =>
            (ServiceConfig)httpContext.RequestServices.GetService(typeof(ServiceConfig));
    }
}