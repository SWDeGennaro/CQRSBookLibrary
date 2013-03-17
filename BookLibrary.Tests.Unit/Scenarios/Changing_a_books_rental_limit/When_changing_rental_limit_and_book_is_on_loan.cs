using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using BookLibrary.EventStore;
using BookLibrary.Events.Books;
using NUnit.Framework;
using BookLibrary.Domain.Members;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_books_rental_limit
{
    public class When_changing_rental_limit_and_book_is_on_loan : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
                new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"),
                rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.Loan();
            book.ChangeRentalLimit(2);
        }

        [Then]
        public void Then_a_cannot_change_rental_limit_exception_is_thrown()
        {
            Assert.AreEqual(typeof(CannotChangeRentalLimitException), CaughtException.GetType());
        }

        [Then]
        public void Then_a_cannot_change_rental_limit_book_out_on_loan_exception_is_thrown_with_message()
        {
            Assert.AreEqual("Cannot change rental limit as book is out on loan", CaughtException.Message);
        }
    }
}
