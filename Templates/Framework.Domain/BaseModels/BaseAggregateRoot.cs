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

        private readonly List<IEvent> _events;

        protected BaseAggregateRoot()
            => _events = new List<IEvent>();

        public BaseAggregateRoot(IEnumerable<IEvent> events)
        {
            if (events is null)
                return;

            foreach (var @event in events)
                ((dynamic)this).On((dynamic)@event);
        }

        protected void AddEvent(IEvent @event)
            => _events.Add(@event);

        public IEnumerable<IEvent> GetEvents()
            => _events.AsEnumerable();

        public void ClearEvents()
            => _events.Clear();
    }
}