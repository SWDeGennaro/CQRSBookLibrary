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
        public Guid BookId { get; private set; }

        public BookTitle Title { get; private set; }

        public Book()
        {
            BookId = Guid.NewGuid();
            Title = new BookTitle(string.Empty, string.Empty, string.Empty, string.Empty);

            registerEvents();
        }

        public Book(BookTitle title) : this()
        {
            Title = title;

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
        }

        #endregion
    }
}
