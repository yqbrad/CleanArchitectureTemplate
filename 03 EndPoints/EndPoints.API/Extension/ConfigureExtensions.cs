using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;
using YQB.Infrastructure.Service.Configuration;

namespace YQB.EndPoints.API.Extension
{
    public static class ConfigureExtensions
    {
        public static void ConfigSwagger(this IApplicationBuilder app, ServiceConfig config)
        {
            if (!config.Swagger.IsEnable)
                return;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(config.Swagger.Url, config.Swagger.Name);
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                c.OAuthClientId(config.Idp.SwaggerClientId);
                c.OAuthAppName(config.Idp.AppName);
                c.OAuthClientSecret(config.Idp.SwaggerClientSecret);
                c.RoutePrefix = config.Swagger.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.DefaultModelRendering(ModelRendering.Example);
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });
        }

        public static void ConfigCors(this IApplicationBuilder app, string[] allowedOrigins)
        {
            if (allowedOrigins is null || allowedOrigins.Length == 0)
                return;

            app.UseCors(x => x
                .WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }

        public static void AddBanner(this IApplicationBuilder app, ServiceConfig config)
        {
            //https://manytools.org/hacker-tools/ascii-banner/
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(config.Banner);
        }
    }
}