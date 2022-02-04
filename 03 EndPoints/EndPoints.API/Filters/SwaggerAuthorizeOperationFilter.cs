using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DDD.EndPoints.API.Filters
{
    public class SwaggerAuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = false;

            if (context.MethodInfo.DeclaringType != null)
                hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any();

            if (!hasAuthorize)
                hasAuthorize = context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any();

            if (!hasAuthorize) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [
                        new OpenApiSecurityScheme {Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        }
                    ] = new[] { "service_api" }
                }
            };
        }
    }
}