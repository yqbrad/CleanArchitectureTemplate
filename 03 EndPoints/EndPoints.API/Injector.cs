using DDD.Contracts._Common;
using DDD.EndPoints.API.Models;
using DDD.Infrastructure.DataAccess._Common;
using DDD.Infrastructure.Service;
using DDD.Infrastructure.Service.Configuration;
using DDD.Infrastructure.Service.Dispatcher;
using DDD.Infrastructure.Service.EventSourcing;
using DDD.Infrastructure.Service.RabbitMq;
using DDD.Infrastructure.Service.Translator;
using Framework.Domain.EventBus;
using Framework.Domain.Events;
using Framework.Domain.Translator;

namespace DDD.EndPoints.API
{
    public static class Injector
    {
        public static void Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserInfo, UserInfo>();
            services.AddSingleton<IUnitOfWorkConfiguration, UnitOfWorkConfig>();
            services.AddDbContext<ServiceDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork<ServiceDbContext>>();
            services.AddEventSourcing(configuration);
            services.AddRabbitMq(configuration);
            //services.AddSingleton<IApiConfiguration, ApiConfiguration>();
            //services.AddSingleton<IApiCaller, ApiCaller>();
            services.AddSingleton<IInternalEventDispatcher, InternalEventDispatcher>();
            services.AddScoped<IServiceBus, ServiceBus>();
            services.AddLocalization();
            services.AddTransient<ITranslator, MicrosoftTranslator>();
        }
    }
}