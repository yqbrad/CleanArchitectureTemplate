using Framework.Domain.EventBus;
using Framework.Domain.Events;

//using Services.Core.Interfaces;

namespace YQB.Infra.Service
{
    public class ServiceBus: IServiceBus
    {
        public IUserInfo UserInfo { get; }

        private readonly IInternalEventDispatcher _internalEventDispatcher;

        public ServiceBus(IInternalEventDispatcher internalEventDispatcher, IUserInfo userInfo)
        {
            UserInfo = userInfo;
            _internalEventDispatcher = internalEventDispatcher;
        }

        //public async Task PublishInternalAsync<TClass, TId>(TClass aggregateRoot)
        //    where TClass : BaseAggregateRoot<TId>
        //    where TId : IEquatable<TId>
        //{
        //    await _internalEventDispatcher.DispatchEventAsync(aggregateRoot.GetEvents().ToArray());
        //}

        //public async Task PublishExternalAsync<TClass, TId>(TClass aggregateRoot)
        //    where TClass : BaseAggregateRoot<TId>
        //    where TId : IEquatable<TId>
        //{
        //    //await _rabbitBusClient.PublishAsync(aggregateRoot.GetEvents());
        //}
    }
}