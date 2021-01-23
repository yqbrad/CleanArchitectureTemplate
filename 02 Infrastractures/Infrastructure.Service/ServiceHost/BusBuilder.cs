using DDD.Infrastructure.Service.RabbitMq;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Commands;
using Framework.Domain.Events;
using Microsoft.AspNetCore.Hosting;
using RawRabbit;

namespace DDD.Infrastructure.Service.ServiceHost
{
    public class BusBuilder : IBuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient busClient)
        {
            _webHost = webHost;
            _bus = busClient;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost
                .Services
                .GetService(typeof(ICommandHandler<TCommand>));

            _bus.WithCommandHandlerAsync(handler);
            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost
                .Services
                .GetService(typeof(IEventHandler<TEvent>));

            _bus.WithEventHandlerAsync(handler);
            return this;
        }

        public override ServiceHost Build() => new ServiceHost(_webHost);
    }
}