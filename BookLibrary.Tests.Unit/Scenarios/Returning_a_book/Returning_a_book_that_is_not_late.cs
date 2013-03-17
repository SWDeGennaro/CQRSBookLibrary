using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.Domain.Books;
using BookLibrary.Domain.Members;
using BookLibrary.Domain;
using NUnit.Framework;
using BookLibrary.Events.Books;

namespace BookLibrary.Tests.Unit.Scenarios.Returning_a_book
{
    public class Returning_a_book_that_is_not_late : AggregateRootTestFixture<Book>
    {
        protected override void When()
        {
            var book = new Book(new BookTitle("Book One", "2322211", "John Doe", "Fiction"), 3);
            
            AggregateRoot = book;

            var member = 
                new Member("Paul", "Smith",
                    new Address("23 Test Street", String.Empty, "Town", "County", "Country", "SK2 4CD"),
                    "34-23-1967");

            AggregateRoot.Loan();
            AggregateRoot.Return();
        }

        [Then]
        public void Then_a_book_returned_event_will_be_fired()
        {
            Assert.AreEqual(typeof(BookReturnedEvent), PublishedEvents.Last().GetType());
        }
    }
}
