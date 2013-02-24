using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using NUnit.Framework;
using BookLibrary.Events;
using BookLibrary.Domain;

namespace BookLibrary.Tests.Unit.Scenarios.Creating_a_new_member
{
    public class When_creating_a_new_member : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith", new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85"); 
        }

        [Then]
        public void Then_a_member_created_event_will_be_raised()
        {
            Assert.AreEqual(typeof(MemberCreatedEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_a_member_created_event_handler_will_be_executed()
        {
            var @event = (MemberCreatedEvent)PublishedEvents.Last();

            Assert.AreEqual("John", @event.FirstName);
            Assert.AreEqual("Smith", @event.LastName);
            Assert.AreEqual("23 Wood Lane", @event.AddressLineOne);
            Assert.AreEqual("", @event.AddressLineTwo);
            Assert.AreEqual("Town", @event.Town);
            Assert.AreEqual("Cheshire", @event.County);
            Assert.AreEqual("United Kingdom", @event.Country);
            Assert.AreEqual("SK23 5NE", @event.PostalCode);
            Assert.AreEqual("02-12-85", @event.DateOfBirth);
        }
    }
}
