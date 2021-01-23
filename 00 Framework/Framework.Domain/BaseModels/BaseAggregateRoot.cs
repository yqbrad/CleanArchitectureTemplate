using System;
using System.Collections.Generic;
using Framework.Domain.Events;

namespace Framework.Domain.BaseModels
{
    public abstract class BaseAggregateRoot<TId> where TId : IEquatable<TId>
    {
        private readonly List<IEvent> _events;
        public TId Id { get; protected set; }
        protected BaseAggregateRoot() => _events = new List<IEvent>();

        protected void HandleEvent(IEvent @event)
        {
            SetStateByEvent(@event);
            ValidateInvariants();
            _events.Add(@event);
        }

        protected abstract void SetStateByEvent(IEvent @event);
        protected abstract void ValidateInvariants();

        public IEvent[] GetEvents() => _events.ToArray();
        public void ClearEvents() => _events.Clear();

        public override bool Equals(object obj)
        {
            var other = obj as BaseAggregateRoot<TId>;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b) => !(a == b);

        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
    }
}