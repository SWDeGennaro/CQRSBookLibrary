using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using BookLibrary.Domain.Books;
using NUnit.Framework;
using BookLibrary.Events.Members;

namespace BookLibrary.Tests.Unit.Scenarios.Member_loaning_a_book
{
    public class Non_suspended_member_loaning_a_book : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
               new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85");

            AggregateRoot.LoanBook("Wind In The Willows", "DKD2929", "John Test", "Fiction", 4);
        }

        [Then]
        public void Then_a_member_loaned_book_event_will_be_raised()
        {
            Assert.AreEqual(typeof(MemberLoanedBookEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_an_event_handler_will_loan_the_book()
        {
            var @event = (MemberLoanedBookEvent)PublishedEvents.Last();

            Assert.AreEqual("Wind In The Willows", @event.Title);
            Assert.AreEqual("DKD2929", @event.Isbn);
            Assert.AreEqual("John Test", @event.Author);
            Assert.AreEqual("Fiction", @event.Category);
            Assert.AreEqual(4, @event.RentalLimit);
        }
    }
}
