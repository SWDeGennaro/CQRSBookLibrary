using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using NUnit.Framework;
using BookLibrary.Domain.Members;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_books_title
{
    public class When_changing_book_title_when_on_loan : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
               new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.Loan(new Member());
            book.ChangeBookTitle("Test Book Two", isbn: "522252", author: "John Jones", category: "Non Fiction");
        }

        [Then]
        public void Then_a_cannot_change_book_title_exception_is_thrown()
        {
            Assert.AreEqual(typeof(CannotChangeBookTitleException), CaughtException.GetType());
        }

        [Then]
        public void Then_a_cannot_change_book_title_exception_is_thrown_with_the_message()
        {
            Assert.AreEqual("Cannot change the title as book is out on loan", CaughtException.Message);
        }
    }
}
