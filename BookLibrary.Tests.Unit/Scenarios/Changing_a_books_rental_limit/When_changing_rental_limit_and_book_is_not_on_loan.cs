using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using BookLibrary.EventStore;
using BookLibrary.Events.Books;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Scenarios.Changing_a_books_rental_limit
{
    public class When_changing_rental_limit_and_book_is_not_on_loan : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            AggregateRoot = new Book(
                new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);

            var book = (Book)AggregateRoot;
            book.ChangeRentalLimit(2);
        }

        [Then]
        public void Then_a_change_rental_limit_event_will_be_raised()
        {
            Assert.AreEqual(typeof(BookRentalLimitChangedEvent), PublishedEvents.Last().GetType());
        }

        [Then]
        public void Then_a_change_rental_limit_event_handler_will_change_the_rental_limit()
        {
            var @event = (BookRentalLimitChangedEvent)PublishedEvents.Last();

            Assert.AreEqual(2, @event.RentalLimit);
        }
    }
}
