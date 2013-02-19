using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using NUnit.Framework;
using BookLibrary.Events.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_books_title
{
    public class When_changing_book_title_when_not_on_loan : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
                new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.ChangeBookTitle("Test Book Two", isbn: "522252", author: "John Jones", category: "Non Fiction");
        }

        [Then]
        public void Then_a_change_book_title_event_is_raised()
        {
            Assert.AreEqual(typeof(ChangeBookTitleEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_a_change_book_title_event_handler_will_be_raised_with_the_following_properties()
        {
            var changeBookTitleEvent = (ChangeBookTitleEvent)PublishedEvents.Last();

            Assert.AreEqual("Test Book Two", changeBookTitleEvent.Title);
            Assert.AreEqual("522252", changeBookTitleEvent.Isbn);
            Assert.AreEqual("John Jones", changeBookTitleEvent.Author);
            Assert.AreEqual("Non Fiction", changeBookTitleEvent.Category);
        }
    }
}
