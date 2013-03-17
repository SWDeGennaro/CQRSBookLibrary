using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore.Storage.Memento;
using BookLibrary.Domain.Members;

namespace BookLibrary.Domain.Mementos
{
    [Serializable]
    public class BookMemento : IMemento
    {
        public Guid BookId { get; private set; }
        public string Title { get; private set; }
        public string Isbn { get; private set; }
        public string Category { get; private set; }
        public string Author { get; private set; }
        public bool OnLoan { get; private set; }
        public int RentalLimit { get; private set; }

        public BookMemento(Guid bookId, string title, string isbn, string category,string author, bool onLoan, int rentalLimit)
        {
            BookId = bookId;
            Title = title;
            Isbn = isbn;
            Category = category;
            Author = author;
            OnLoan = onLoan;
            RentalLimit = rentalLimit;
        }
    }
}
