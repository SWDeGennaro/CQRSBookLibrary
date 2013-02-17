using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.EventStore.Storage.Mementos;
using BookLibrary.Events.Books;

namespace BookLibrary.Domain.Books
{
    public class Book : BaseAggregateRoot<IDomainEvent>, IOriginator
    {
        private Guid _bookId;
        private BookTitle _title;

        public Book()
        {
            _bookId = Guid.NewGuid();
            _title = new BookTitle(string.Empty, string.Empty, string.Empty, string.Empty);

            registerEvents();
        }

        public Book(BookTitle title) : this()
        {
            Apply(new BookRegisteredEvent(Guid.NewGuid(), title.Title, title.Isbn, title.Author, title.Category));
        }

        #region Memento

        public EventStore.Storage.Memento.IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(EventStore.Storage.Memento.IMemento memento)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region internal event handlers

        private void registerEvents()
        {
            RegisterEvent<BookRegisteredEvent>(onBookRegisteredEvent);
        }

        private void onBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            _bookId = bookRegisteredEvent.BookId;
            _title = new BookTitle(bookRegisteredEvent.Title, bookRegisteredEvent.Isbn, bookRegisteredEvent.Author, bookRegisteredEvent.Category);
        }

        #endregion
    }
}
