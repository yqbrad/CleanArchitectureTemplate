using System;
using System.Threading.Tasks;
using Framework.Domain.BaseModels;
using Framework.Domain.Data;
using Framework.Domain.EventBus;
using Framework.Domain.Events;
using RawRabbit;
using Services.Core.Interfaces;

namespace $safeprojectname$
{
    public class EventBus : IEventBus
    {
        public IApplicant Applicant { get; }

        private readonly IApiCaller _apiCaller;
        private readonly IBusClient _rabbitBusClient;
        private readonly IEventSource _eventSource;
        private readonly IInternalEventDispatcher _internalEventDispatcher;

        public EventBus(IInternalEventDispatcher internalEventDispatcher, IBusClient busClient,
            IEventSource eventSource, IApiCaller apiCaller, IApplicant applicant)
        {
            Applicant = applicant;

            _internalEventDispatcher = internalEventDispatcher;
            _rabbitBusClient = busClient;
            _eventSource = eventSource;
            _apiCaller = apiCaller;
        }

        public ApiServices GetApiServices()
        {
            _apiCaller.SetAuthorizationToken("bearer", Applicant.Token);
            return new ApiServices();
        }

        public async Task PublishInternalAsync<TClass, TId>(TClass aggregateRoot)
            where TClass : BaseAggregateRoot<TId>
            where TId : IEquatable<TId>
        {
            await _internalEventDispatcher.DispatchEventAsync(aggregateRoot.GetEvents());
            await Log<TClass, TId>(aggregateRoot);
        }

        public async Task PublishExternalAsync<TClass, TId>(TClass aggregateRoot)
            where TClass : BaseAggregateRoot<TId>
            where TId : IEquatable<TId>
        {
            await _rabbitBusClient.PublishAsync(aggregateRoot.GetEvents());
            await Log<TClass, TId>(aggregateRoot);
        }

        private async Task Log<TClass, TId>(TClass aggregateRoot)
            where TClass : BaseAggregateRoot<TId>
            where TId : IEquatable<TId>
        {
            try
            {
                await _eventSource.SaveAsync(aggregateRoot.GetType().FullName,
                    aggregateRoot.Id.ToString(), aggregateRoot.GetEvents());
            }
            catch (Exception)
            {
                //TODO Log EventSource Not Work
            }
        }
    }
}