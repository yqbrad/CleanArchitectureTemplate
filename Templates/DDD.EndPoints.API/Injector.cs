using DDD.Contracts._Base;
using $safeprojectname$.Configuration;
using $safeprojectname$.Extension;
using $safeprojectname$.Models;
using DDD.Infrastructure.DataAccess._Base;
using DDD.Infrastructure.Service;
using DDD.Infrastructure.Service.DB;
using DDD.Infrastructure.Service.Dispatcher;
using DDD.Infrastructure.Service.EventSourcing;
using DDD.Infrastructure.Service.RabbitMq;
using Framework.Domain.EventBus;
using Framework.Domain.Events;
using Logger.EndPoints.Service.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$
{
    public static class Injector
    {
        public static void Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IApplicant, Applicant>();
            services.AddLogger();
            services.AddSingleton<IUnitOfWorkConfiguration, UnitOfWorkConfig>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DatabaseInitializer>();
            services.AddEventSourcing(configuration);
            services.AddRabbitMq(configuration);
            services.AddApiConfig();
            services.AddSingleton<IInternalEventDispatcher, InternalEventDispatcher>();
            services.AddScoped<IEventBus, EventBus>();


        }
    }
}