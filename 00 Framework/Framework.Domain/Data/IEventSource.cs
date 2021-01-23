using Framework.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Domain.Data
{
    public interface IEventSource
    {
        Task SaveAsync<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events)
           where TEvent : IEvent;
    }
}