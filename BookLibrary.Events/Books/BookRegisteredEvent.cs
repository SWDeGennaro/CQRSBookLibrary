using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class BookRegisteredEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public int RentalLimit { get; private set; }
        public string Title { get; private set; }
        public string Isbn { get; private set; }
        public string Author { get; private set; }
        public string Category { get; private set; }

        public BookRegisteredEvent(Guid bookId, int rentalLimit, string title, string isbn, string author, string category)
        {
            BookId = bookId;
            RentalLimit = rentalLimit;
            Title = title;
            Isbn = isbn;
            Author = author;
            Category = category;
        }
    }
}
