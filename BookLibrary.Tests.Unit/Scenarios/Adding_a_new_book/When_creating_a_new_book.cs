using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using BookLibrary.EventStore;
using NUnit.Framework;
using BookLibrary.Events.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Adding_a_new_book
{

    public class When_creating_a_new_book : AggregateRootTestFixture<Book>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            AggregateRoot = new Book(
                new BookTitle(title: "Test Book", isbn: "29292929339", author: "John Smith", category: "Fiction"), rentalLimit: 4);
        }

        [Then]
        public void Then_a_book_registered_event_will_be_published()
        {
            Assert.AreEqual(typeof(BookRegisteredEvent), PublishedEvents.Last().GetType());            
        }

        [Then]
        public void Then_a_book_registered_event_handler_will_be_fired_to_change_the_state_of_the_book()
        {
            var @event = (BookRegisteredEvent)PublishedEvents.Last();

            Assert.AreEqual("Test Book", @event.Title);
            Assert.AreEqual("29292929339", @event.Isbn);
            Assert.AreEqual("John Smith", @event.Author);
            Assert.AreEqual("Fiction", @event.Category);
            Assert.AreEqual(4, @event.RentalLimit);
        }
    }
}
