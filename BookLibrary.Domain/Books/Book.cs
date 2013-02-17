using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.EventStore.Storage.Mementos;
using BookLibrary.Events.Books;
using BookLibrary.Domain.Members;

namespace BookLibrary.Domain.Books
{
    public class Book : BaseAggregateRoot<IDomainEvent>, IOriginator
    {
        private Guid _bookId;
        private BookTitle _title;
        private int _rentalLimt;
        private Member _member;

        public Book()
        {
            _bookId = Guid.NewGuid();
            _title = new BookTitle(string.Empty, string.Empty, string.Empty, string.Empty);

            registerEvents();
        }

        public Book(BookTitle title, int rentalLimit) : this()
        {
            Apply(new BookRegisteredEvent(Guid.NewGuid(), rentalLimit, title.Title, title.Isbn, title.Author, title.Category));
        }

        public void ChangeRentalLimit(int rentalLimit)
        {
            canChangeRentalLimit();

            Apply(new ChangeBookRentalLimitEvent(rentalLimit));
        }

        private void canChangeRentalLimit()
        {
            if (isBookOnLoan())
                throw new CannotChangeRentalLimitException("Cannot change rental limit as book is out on loan");
        }

        private bool isBookOnLoan()
        {
            return _member != null
                ? true
                : false;
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
            RegisterEvent<ChangeBookRentalLimitEvent>(onChangeBookRentalLimitEvent);
        }

        private void onBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            _bookId = bookRegisteredEvent.BookId;
            _title = new BookTitle(bookRegisteredEvent.Title, bookRegisteredEvent.Isbn, bookRegisteredEvent.Author, bookRegisteredEvent.Category);
        }

        private void onChangeBookRentalLimitEvent(ChangeBookRentalLimitEvent changeBookRentalLimitEvent)
        {
            _rentalLimt = changeBookRentalLimitEvent.RentalLimit;
        }

        #endregion
    }
}
