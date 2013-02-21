using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BookLibrary.Domain.Books;
using BookLibrary.Domain.Members;
using BookLibrary.Events.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Loaning_a_book
{
    [TestFixture]
    public class Loaning_a_book_that_is_not_out_on_loan_already : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
                 new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.Loan(new Member());
        }

        [Then]
        public void Then_a_book_loaned_event_will_be_raised()
        {
            Assert.AreEqual(typeof(BookLoanedEvent), PublishedEvents.Last().GetType());
        }
    }
}
