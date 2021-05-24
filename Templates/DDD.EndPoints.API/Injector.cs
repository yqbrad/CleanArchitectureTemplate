using DDD.Contracts._Base;
using $safeprojectname$.Configuration;
using $safeprojectname$.Models;
using DDD.Infrastructure.DataAccess._Base;
using DDD.Infrastructure.Service;
using DDD.Infrastructure.Service.ApiConfig;
using DDD.Infrastructure.Service.Dispatcher;
using DDD.Infrastructure.Service.EventSourcing;
using DDD.Infrastructure.Service.RabbitMq;
using Framework.Domain.EventBus;
using Framework.Domain.Events;
using Logger.EndPoints.Service.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;
using Services.WebApiCaller;

namespace $safeprojectname$
{
    public static class Injector
    {
        public static void Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IApplicant, Applicant>();
            services.AddSingleton<IUnitOfWorkConfiguration, UnitOfWorkConfig>();
            services.AddDbContext<ServiceDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork<ServiceDbContext>>();
            services.AddEventSourcing(configuration);
            services.AddRabbitMq(configuration);
            services.AddSingleton<IApiConfiguration, ApiConfiguration>();
            services.AddSingleton<IApiCaller, ApiCaller>();
            services.AddLogger();
            services.AddSingleton<IInternalEventDispatcher, InternalEventDispatcher>();
            services.AddScoped<IEventBus, EventBus>();

        }
    }
}