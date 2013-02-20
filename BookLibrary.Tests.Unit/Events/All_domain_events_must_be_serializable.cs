using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Events
{
    [TestFixture]
    public class All_domain_events_must_be_serializable
    {
        [Test]
        public void All_domain_events_will_have_the_Serializable_attribute_assigned()
        {
            var domainEventTypes = 
                typeof(global::BookLibrary.Events.DomainEvent)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(global::BookLibrary.Events.DomainEvent))
                .ToList();

            foreach (var domainEventType in domainEventTypes)
            {
                if (domainEventType.IsSerializable)
                    continue;

                throw new Exception(string.Format("Domain event '{0}' is not Serializable", domainEventType.FullName));
            }
        }
    }
}
