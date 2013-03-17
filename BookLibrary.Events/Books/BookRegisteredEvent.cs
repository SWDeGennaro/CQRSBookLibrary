using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class BookRegisteredEvent : DomainEvent
    {
        public readonly Guid BookId;
        public readonly int RentalLimit;
        public readonly string Title;
        public readonly string Isbn;
        public readonly string Author;
        public readonly string Category;

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
