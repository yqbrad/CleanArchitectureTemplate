using Framework.Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YQB.Infrastructure.Service.EventSourcing
{
    public static class Extensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services, IConfiguration configuration)
        {
            var option = new EventSourcingOptions();
            var section = configuration.GetSection("EventStore");
            section.Bind(option);

            services.Configure<EventSourcingOptions>(section);
            services.AddSingleton<IEventSource, EventSourceInitializer>();

            return services;
        }
    }
}