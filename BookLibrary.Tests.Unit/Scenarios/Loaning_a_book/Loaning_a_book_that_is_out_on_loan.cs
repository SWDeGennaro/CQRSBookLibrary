using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using BookLibrary.Domain.Members;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Scenarios.Loaning_a_book
{
    public class Loaning_a_book_that_is_out_on_loan : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
               new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.Loan();

            //book already out on loan
            book.Loan();
        }

        [Then]
        public void Then_a_book_cannot_be_loaned_exception_should_be_thrown()
        {
            Assert.AreEqual(typeof(CannotLoanBookException), CaughtException.GetType());
        }

    }
}
