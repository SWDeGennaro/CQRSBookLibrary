using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;
using BookLibrary.Domain.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_members_address
{
    public class Changing_a_members_address_when_a_book_is_out_on_loan : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
                new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85");

            AggregateRoot.LoanBook("Wind In The Willows", "DKD2929", "John Test", "Fiction", 4);

            AggregateRoot.ChangeAddress("25 Springs Lane", "The Grove", "Denton", "Lancashire", "United Kingdom", "M25 3RT");
        }

        [Then]
        public void Then_a_cannot_change_members_address_until_all_books_are_returned_exception_is_thrown()
        {
            Assert.AreEqual(typeof(CannotChangeMemberAddressException), CaughtException.GetType());
        }

        [Then]
        public void Then_the_exceptions_message_will_be()
        {
            Assert.AreEqual("Cannot change address while books are out on loan", CaughtException.Message);
        }
    }
}
