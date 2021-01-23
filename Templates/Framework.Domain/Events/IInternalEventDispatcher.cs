using System.Threading.Tasks;

namespace $safeprojectname$.Events
{
    public interface IInternalEventDispatcher
    {
        Task DispatchEventAsync<T>(T @event)
            where T : IEvent;

        Task DispatchEventAsync<T>(params T[] events)
            where T : IEvent;
    }
}