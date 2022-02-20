using $safeprojectname$.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace $safeprojectname$.BaseModels
{
    public abstract class BaseAggregateRoot<TId>
        where TId : IEquatable<TId>
    {
        public TId Id { get; protected set; }

        private readonly List<IDomainEvent> _events;

        protected BaseAggregateRoot()
            => _events = new List<IDomainEvent>();

        public BaseAggregateRoot(IEnumerable<IDomainEvent> events)
        {
            if (events is null)
                return;

            foreach (var @event in events)
                ((dynamic)this).On((dynamic)@event);
        }

        protected void AddEvent(IDomainEvent @event)
            => _events.Add(@event);

        public IEnumerable<IDomainEvent> GetEvents()
            => _events.AsEnumerable();

        public void ClearEvents()
            => _events.Clear();
    }
}