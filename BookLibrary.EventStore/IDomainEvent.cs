using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.EventStore
{
    /// <summary>
    /// Represents the contract for domain events
    /// </summary>
    public interface IDomainEvent
    {
        Guid Id { get; }
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}
