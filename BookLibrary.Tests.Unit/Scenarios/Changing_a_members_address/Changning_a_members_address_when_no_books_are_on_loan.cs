using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;
using BookLibrary.Events.Members;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_members_address
{
    public class Changning_a_members_address_when_no_books_are_on_loan : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
                new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85");

            AggregateRoot.ChangeAddress("25 Springs Lane", "The Grove", "Denton", "Lancashire", "United Kingdom", "M25 3RT");
        }

        [Then]
        public void Then_a_change_address_event_will_be_raised()
        {
            Assert.AreEqual(typeof(MemberAddressChangedEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_an_event_handler_will_change_the_members_address()
        {
            var @event = (MemberAddressChangedEvent)PublishedEvents.Last();

            Assert.AreEqual("25 Springs Lane", @event.AddressLineOne);
            Assert.AreEqual("The Grove", @event.AddressLineTwo);
            Assert.AreEqual("Denton", @event.Town);
            Assert.AreEqual("Lancashire", @event.County);
            Assert.AreEqual("United Kingdom", @event.Country);
            Assert.AreEqual("M25 3RT", @event.PostalCode);
        }
    }
}
