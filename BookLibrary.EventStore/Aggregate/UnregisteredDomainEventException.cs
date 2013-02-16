using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.EventStore.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message)
        {

        }
    }
}
