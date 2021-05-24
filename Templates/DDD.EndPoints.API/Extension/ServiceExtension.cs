using $safeprojectname$.Configuration;
using $safeprojectname$.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace $safeprojectname$.Extension
{
    public static class ServiceExtension
    {
        public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var uow = new UnitOfWorkConfig(configuration);
            services.AddHealthChecks()
                .AddSqlServer(uow.SqlServerConnectionString, tags: new List<string> { "DB" });
        }

        public static void AddSwagger(this IServiceCollection services, ServiceConfig config)
        {
            if (!config.Swagger.IsEnable)
                return;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(config.Swagger.Version,
                    new OpenApiInfo
                    {
                        Title = config.Swagger.Title,
                        Version = config.Swagger.Version
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<SwaggerAuthorizeOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(config.Idp.AuthorizationUrl),
                            TokenUrl = new Uri(config.Idp.TokenUrl),
                            Scopes = config.Idp.Scopes
                        }
                    }
                });
            });
        }

        public static void AddIdp(this IServiceCollection services, ServiceConfig config)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ApiName = config.Idp.ApiName;
                    options.Authority = config.Idp.ServerUrl;
                    options.RequireHttpsMetadata = config.Idp.RequireHttps;
                    options.JwtValidationClockSkew = TimeSpan.Zero;
                });
        }

        public static void AddHeaderPropagation(this IServiceCollection services, ServiceConfig config)
        {
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Authorization", context =>
                {
                    var isExistToken = context.HttpContext.Request.Headers
                        .TryGetValue("Authorization", out var token);
                    return isExistToken ? new StringValues(token.ToString()) : new StringValues();
                });
            });
        }

        public static ServiceConfig AddServiceConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            var serviceConfig = new ServiceConfig();
            configuration.GetSection(nameof(ServiceConfig)).Bind(serviceConfig);
            services.AddSingleton(serviceConfig);

            return serviceConfig;
        }

        public static void ConfigSwagger(this IApplicationBuilder app, ServiceConfig config)
        {
            if (!config.Swagger.IsEnable)
                return;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(config.Swagger.Url, config.Swagger.Name);
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
    }
}