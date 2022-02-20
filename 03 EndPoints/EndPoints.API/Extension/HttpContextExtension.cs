using YQB.Infrastructure.Service.Configuration;

namespace YQB.EndPoints.API.Extension
{
    public static class HttpContextExtension
    {
        public static ServiceConfig ServiceContext(this HttpContext httpContext) =>
            (ServiceConfig)httpContext.RequestServices.GetService(typeof(ServiceConfig));

        public static IServiceProvider ServiceProvider(this HttpContext httpContext) =>
            (IServiceProvider)httpContext.RequestServices.GetService(typeof(IServiceProvider));
    }
}