using YQB.Contracts._Common;
using YQB.EndPoints.API.Filters;
using YQB.EndPoints.API.Models;
using YQB.Infrastructure.DataAccess._Common;
using YQB.Infrastructure.Service;
using YQB.Infrastructure.Service.Configuration;
using YQB.Infrastructure.Service.Dispatcher;
using YQB.Infrastructure.Service.EventSourcing;
using YQB.Infrastructure.Service.RabbitMq;
using YQB.Infrastructure.Service.Translator;
using FluentValidation;
using Framework.Domain.ApplicationServices;
using Framework.Domain.EventBus;
using Framework.Domain.Events;
using Framework.Domain.Translator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

namespace YQB.EndPoints.API.Extension;

public static class ServiceExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        IConfiguration configuration, ServiceConfig serviceConfig)
    {
        var assemblies = GetAssemblies(new[] { serviceConfig.AssemblyName }).ToList();
        return services
            .AddIdp(serviceConfig)
            .AddRepositories(assemblies)
            .AddHandlers(assemblies)
            .AddValidatorsFromAssemblies(assemblies)
            .AddHttpContextAccessor()
            .AddUserInfo()
            .AddUnitOfWorkConfig()
            .AddDbContext()
            .AddUnitOfWork()
            .AddEventSourcing(configuration)
            .AddRabbitMq(configuration)
            .AddInternalEventDispatcher()
            .AddServiceBus()
            .AddLocalization()
            .AddTranslator()
            .AddResponseCaching()
            .AddHeaderPropagation(serviceConfig)
            .AddSwagger(serviceConfig)
            .AddHealthCheck(configuration);

        //services.AddSingleton<IApiConfiguration, ApiConfiguration>();
        //services.AddSingleton<IApiCaller, ApiCaller>();
    }

    public static void AddAppsettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

        var buildConfiguration = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
        var configuration = buildConfiguration == "Debug" ? "Development" : buildConfiguration;

        builder.Configuration.AddJsonFile($"appsettings.{configuration}.json", false, true);
        builder.Configuration.AddJsonFile($"appsettings.{configuration}.serilog.json", false, true);
    }

    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var uow = new UnitOfWorkConfig(configuration);
        services.AddHealthChecks()
            .AddSqlServer(uow.SqlServerConnectionString, tags: new List<string> { "DB" });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, ServiceConfig config)
    {
        if (!config.Swagger.IsEnable)
            return services;
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(config.Swagger.Version,
                new OpenApiInfo
                {
                    Title = config.Swagger.Title,
                    Version = config.Swagger.Version,
                    Contact = new OpenApiContact
                    {
                        Name = "Health Check",
                        Url = new Uri($"http://localhost:3070{config.HealthCheckRoute}")
                    }
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

        return services;
    }

    public static IServiceCollection AddIdp(this IServiceCollection services, ServiceConfig config)
    {
        if (!config.Idp.IsEnable)
            return services;

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.ApiName = config.Idp.ApiName;
                options.Authority = config.Idp.ServerUrl;
                options.RequireHttpsMetadata = config.Idp.RequireHttps;
                options.JwtValidationClockSkew = TimeSpan.Zero;
            });

        return services;
    }

    public static IServiceCollection AddHeaderPropagation(this IServiceCollection services, ServiceConfig config)
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

        return services;
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
        => services.AddWithTransientLifetime(assemblies, typeof(IRepository<,>));

    public static IServiceCollection AddHandlers(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
        => services.AddWithTransientLifetime(assemblies,
            typeof(IRequestHandler<>), typeof(IRequestHandler<,>), typeof(IEventHandler<>));

    public static IServiceCollection AddUserInfo(this IServiceCollection services)
        => services.AddScoped<IUserInfo, UserInfo>();

    public static IServiceCollection AddUnitOfWorkConfig(this IServiceCollection services)
        => services.AddSingleton<IUnitOfWorkConfiguration, UnitOfWorkConfig>();

    public static IServiceCollection AddDbContext(this IServiceCollection services)
        => services.AddDbContext<ServiceDbContext>();

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        => services.AddTransient<IUnitOfWork, UnitOfWork<ServiceDbContext>>();

    public static IServiceCollection AddInternalEventDispatcher(this IServiceCollection services)
        => services.AddSingleton<IInternalEventDispatcher, InternalEventDispatcher>();

    public static IServiceCollection AddServiceBus(this IServiceCollection services)
        => services.AddScoped<IServiceBus, ServiceBus>();

    public static IServiceCollection AddTranslator(this IServiceCollection services)
        => services.AddSingleton<ITranslator, MicrosoftTranslator>();

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