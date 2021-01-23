using $safeprojectname$.Configuration;
using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Extension
{
    public static class HttpContextExtension
    {
        public static ServiceConfig ServiceContext(this HttpContext httpContext) =>
            (ServiceConfig)httpContext.RequestServices.GetService(typeof(ServiceConfig));
    }
}