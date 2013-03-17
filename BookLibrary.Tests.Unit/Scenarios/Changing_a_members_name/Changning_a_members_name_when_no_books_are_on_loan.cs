using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;
using BookLibrary.Events.Members;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_members_name
{
    public class Changning_a_members_name_when_no_books_are_on_loan : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
                new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85"); 

            AggregateRoot.ChangeName("Peter", "Jones");
        }

        [Then]
        public void Then_a_change_name_event_will_be_raised()
        {
            Assert.AreEqual(typeof(MemberNameChangedEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_an_event_handler_will_change_the_members_name()
        {
            var @event = (MemberNameChangedEvent)PublishedEvents.Last();

            Assert.AreEqual("Peter", @event.FirstName);
            Assert.AreEqual("Jones", @event.LastName);
        }
    }
}
