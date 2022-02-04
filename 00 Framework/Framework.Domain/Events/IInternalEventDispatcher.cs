using System.Threading.Tasks;

namespace Framework.Domain.Events
{
    public interface IInternalEventDispatcher
    {
        Task DispatchEventAsync<T>(T @event)
            where T : IDomainEvent;

        Task DispatchEventAsync<T>(params T[] events)
            where T : IDomainEvent;
    }
}