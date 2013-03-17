using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class BookLoanedEvent : DomainEvent
    {
        public readonly Guid BookId;

        public BookLoanedEvent(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
