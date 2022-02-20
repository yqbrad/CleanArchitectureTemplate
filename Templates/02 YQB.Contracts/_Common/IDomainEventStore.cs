using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Domain.Events;

namespace $safeprojectname$._Common
{
    public interface IDomainEventStore
    {
        void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events)
            where TEvent : IDomainEvent;

        Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events)
            where TEvent : IDomainEvent;
    }
}