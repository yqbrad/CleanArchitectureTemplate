using System.Reflection;
using System.Threading.Tasks;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Commands;
using Framework.Domain.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;

namespace DDD.Infrastructure.Service.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg
                => handler.HandleAsync(msg), ctx
                => ctx.UseConsumerConfiguration(cfg
                    => cfg.FromDeclaredQueue(q
                        => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler)
            where TEvent : IDomainEvent
            => bus.SubscribeAsync<TEvent>(msg
                => handler.HandleAsync(msg), ctx
                => ctx.UseConsumerConfiguration(cfg
                    => cfg.FromDeclaredQueue(q
                        => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("RabbitMq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(client);

            return services;
        }
    }
}