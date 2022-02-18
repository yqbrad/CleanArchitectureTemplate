using DDD.Contracts._Common;
using DDD.EndPoints.API.Filters;
using DDD.Infrastructure.Service.Configuration;
using FluentValidation;
using Framework.Domain.ApplicationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

namespace DDD.EndPoints.API.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,
            params string[] assemblyNamesForSearch)
        {
            var assemblies = GetAssemblies(assemblyNamesForSearch).ToList();
            return services
                .AddRepositories(assemblies)
                .AddHandlers(assemblies)
                .AddValidatorsFromAssemblies(assemblies);
        }

        public static void AddAppsettings(this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

            var buildConfiguration = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
            var configuration = buildConfiguration == "Debug" ? "Development" : buildConfiguration;

            builder.Configuration.AddJsonFile($"appsettings.{configuration}.json", false, true);
            builder.Configuration.AddJsonFile($"appsettings.{configuration}.serilog.json", false, true);
        }

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
                c.SchemaFilter<SwaggerEnumSchemaFilter>();
                c.SchemaFilter<SwaggerDefaultValueFilter>();
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
            if (!config.Idp.IsEnable)
                return;

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

        public static void AddBanner(this IApplicationBuilder app, ServiceConfig config)
        {
            //https://manytools.org/hacker-tools/ascii-banner/
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(config.Banner);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
            => services.AddWithTransientLifetime(assemblies,
                typeof(IRepository<,>));

        public static IServiceCollection AddHandlers(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
            => services.AddWithTransientLifetime(assemblies,
                typeof(IRequestHandler<>), typeof(IRequestHandler<,>), typeof(IEventHandler<>));

        public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assemblies, params Type[] assignableTo)
            => services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assemblies, params Type[] assignableTo)
            => services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assemblies, params Type[] assignableTo)
            => services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

        private static IEnumerable<Assembly> GetAssemblies(string[] assembliesName)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
                if (IsCandidateCompilationLibrary(library, assembliesName))
                    assemblies.Add(Assembly.Load(new AssemblyName(library.Name)));

            return assemblies;
        }

        private static bool IsCandidateCompilationLibrary(Library library,
            string[] assemblyName)
            => assemblyName.Any(s => library.Name.Contains(s)) ||
               library.Dependencies.Any(dependency => assemblyName.Any(x => dependency.Name.Contains(x)));
    }
}