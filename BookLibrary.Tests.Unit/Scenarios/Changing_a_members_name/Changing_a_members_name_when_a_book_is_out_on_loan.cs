using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;
using BookLibrary.Domain.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_members_name
{
    public class Changing_a_members_name_when_a_book_is_out_on_loan : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
                new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85");

            AggregateRoot.LoanBook("Wind In The Willows", "DKD2929", "John Test", "Fiction", 4);

            AggregateRoot.ChangeName("Peter", "Jones");
        }

        [Then]
        public void Then_a_cannot_change_members_name_until_all_books_are_returned_exception_is_thrown()
        {
            Assert.AreEqual(typeof(CannotChangeMemberNameException), CaughtException.GetType());
        }

        [Then]
        public void Then_the_exceptions_message_will_be()
        {
            Assert.AreEqual("Cannot change name while books are out on loan", CaughtException.Message);
        }
    }
}
