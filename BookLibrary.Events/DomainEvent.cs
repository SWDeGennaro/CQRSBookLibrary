using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;

namespace BookLibrary.Events
{
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        int IDomainEvent.Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
