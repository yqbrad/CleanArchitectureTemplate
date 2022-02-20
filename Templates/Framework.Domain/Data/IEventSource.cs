using $safeprojectname$.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Data
{
    public interface IEventSource
    {
        Task SaveAsync<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events)
           where TEvent : IDomainEvent;
    }
}