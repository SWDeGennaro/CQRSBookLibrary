using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.EventStore.Storage.Mementos;
using BookLibrary.Events.Books;
using BookLibrary.Domain.Members;
using BookLibrary.EventStore.Storage.Memento;
using BookLibrary.Domain.Mementos;

namespace BookLibrary.Domain.Books
{
    public class Book : BaseAggregateRoot<IDomainEvent>, IOriginator
    {
        private Guid _bookId;
        private BookTitle _title;
        private int _rentalLimt;
        private Member _onLoanTo;

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

            Apply(new BookRentalLimitChangedEvent(rentalLimit));
        }

        public void ChangeBookTitle(string title, string isbn, string author, string category)
        {
            canChangeTitle();

            Apply(new BookTitleChangedEvent(title, isbn, author, category));
        }

        public void Loan(Member member)
        {
            canLoanBook();

            Apply(new BookLoanedEvent(
                member.MemberId, member.FirstName, member.LastName, member.Address.AddressLineOne, 
                member.Address.AddressLineTwo, member.Address.Town, member.Address.County, 
                member.Address.Country, member.Address.PostalCode, member.DateOfBirth));
        }

        private void canChangeRentalLimit()
        {
            if (isBookOnLoan())
                throw new CannotChangeRentalLimitException("Cannot change rental limit as book is out on loan");
        }

        private void canChangeTitle()
        {
            if (isBookOnLoan())
                throw new CannotChangeBookTitleException("Cannot change the title as book is out on loan");
        }

        private void canLoanBook()
        {
            if (isBookOnLoan())
                throw new CannotLoanBookException("Cannot loan this book it is already on loan");
        }

        private bool isBookOnLoan()
        {
            return _onLoanTo != null
                ? true
                : false;
        }

        #region Memento

        public IMemento CreateMemento()
        {
            return new BookMemento(_bookId, _title.Title, _title.Isbn, _title.Category, _title.Author, _onLoanTo, _rentalLimt);
        }

        public void SetMemento(IMemento memento)
        {
            var bookMemento = (BookMemento)memento;

            _title = new BookTitle(bookMemento.Title, bookMemento.Isbn, bookMemento.Author, bookMemento.Category);
            _rentalLimt = bookMemento.RentalLimit;
            _onLoanTo = bookMemento.OnLoanTo;
        }

        #endregion

        #region internal event handlers

        private void registerEvents()
        {
            RegisterEvent<BookRegisteredEvent>(onBookRegisteredEvent);
            RegisterEvent<BookRentalLimitChangedEvent>(onBookRentalLimitChangedEvent);
            RegisterEvent<BookTitleChangedEvent>(onBookTitleChangedEvent);
            RegisterEvent<BookLoanedEvent>(onBookLoanedEvent);
        }

        private void onBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            _bookId = bookRegisteredEvent.BookId;
            _title = new BookTitle(bookRegisteredEvent.Title, bookRegisteredEvent.Isbn, bookRegisteredEvent.Author, bookRegisteredEvent.Category);
            _rentalLimt = bookRegisteredEvent.RentalLimit;
        }

        private void onBookRentalLimitChangedEvent(BookRentalLimitChangedEvent changeBookRentalLimitEvent)
        {
            _rentalLimt = changeBookRentalLimitEvent.RentalLimit;
        }

        private void onBookTitleChangedEvent(BookTitleChangedEvent changeBookTitleEvent)
        {
            _title =
                new BookTitle(changeBookTitleEvent.Title, changeBookTitleEvent.Isbn, changeBookTitleEvent.Author, changeBookTitleEvent.Category);
        }

        private void onBookLoanedEvent(BookLoanedEvent bookLoanedEvent)
        {
            _onLoanTo = new Member(bookLoanedEvent.FirstName, bookLoanedEvent.LastName, 
                new Address(bookLoanedEvent.AddressLineOne, bookLoanedEvent.AddressLineTwo, bookLoanedEvent.Town,
                    bookLoanedEvent.County, bookLoanedEvent.Country, bookLoanedEvent.PostalCode), bookLoanedEvent.DateOfBirth);
        }

        #endregion
    }
}
