using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.EventStore.Aggregate
{
    /// <summary>
    /// base class that all aggregate roots inherit from 
    /// </summary>
    /// <typeparam name="TDomainEvent">Type of the Aggregate root</typeparam>
    public abstract class BaseAggregateRoot<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly List<TDomainEvent> _appliedEvents = new List<TDomainEvent>();
        private readonly Dictionary<Type, Action<TDomainEvent>> _registeredEvents;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public BaseAggregateRoot()
        {
            _appliedEvents = new List<TDomainEvent>();
            _registeredEvents = new Dictionary<Type, Action<TDomainEvent>>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            _registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        public void MarkChangesAsCommitted()
        {
            _appliedEvents.Clear();
        }

        public void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            if (domainEvents.Count() == 0)
                return;

            foreach (var domainEvent in domainEvents) 
                apply(domainEvent.GetType(), domainEvent);

            Version = domainEvents.Last().Version;
            EventVersion = Version;
        }

        public IEnumerable<TDomainEvent> GetAppliedEvents()
        {
            return _appliedEvents;
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = GetNewEventVersion();
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        private void apply(Type eventType, TDomainEvent domainEvent)
        {
            Action<TDomainEvent> handler;

            if (!_registeredEvents.TryGetValue(eventType, out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }

        private int GetNewEventVersion()
        {
            return ++EventVersion;
        }
    }
}
