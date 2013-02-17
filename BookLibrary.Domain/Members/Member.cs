using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;

namespace BookLibrary.Domain.Members
{
    public class Member : BaseAggregateRoot<IDomainEvent>
    {
    }
}
