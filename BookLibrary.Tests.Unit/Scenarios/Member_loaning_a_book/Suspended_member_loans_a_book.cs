using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Events;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Scenarios.Member_loaning_a_book
{
    public class Suspended_member_loans_a_book : AggregateRootTestFixture<Member>
    {
        protected override void When()
        {
            AggregateRoot = new Member("John", "Smith",
               new Address("23 Wood Lane", String.Empty, "Hyde", "Cheshire", "United Kingdom", "SK23 5NE"), "02-12-85");

            AggregateRoot.LoanBook("Wind In The Willows", "DKD2929", "John Test", "Fiction", 4);
        }

        [Then]
        public void Then_a_member_is_suspended_exception_is_thrown()
        {
            Assert.AreEqual(typeof(MemberIsSuspendedException), CaughtException.GetType());
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            Assert.AreEqual("The current member is suspended", CaughtException.Message);
        }
    }
}
