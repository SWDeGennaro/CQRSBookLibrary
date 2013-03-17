using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.Events;
using BookLibrary.Events.Members;
using BookLibrary.Domain.Books;

namespace BookLibrary.Domain.Members
{
    public class Member : BaseAggregateRoot<IDomainEvent>
    {
        private Guid _memberId;
        private string _firstName;
        private string _lastName;
        private Address _address;
        private string _dateOfBirth;
        private List<Book> _loanedBooks;
        private bool _isMemberSuspended;

        public Member()
        {
            registerEvents();
            _memberId = Guid.NewGuid();
            _loanedBooks = new List<Book>();
            base.Id = _memberId;
            _isMemberSuspended = false;
        }

        public Member(string firstName, string lastName, Address address, string dateOfBirth) : this()
        {
            Apply(new MemberCreatedEvent(
                Guid.NewGuid(), firstName, lastName, address.AddressLineOne, address.AddressLineTwo, 
                address.Town, address.County, address.Country, address.PostalCode, dateOfBirth));
        }

        public void ChangeName(string firstName, string lastName)
        {
            canChangeName();

            Apply(new MemberNameChangedEvent(firstName, lastName));
        }

        public void ChangeAddress(string addressLineOne, string addressLineTwo, string town,
            string county, string country, string postalCode)
        {
            canChangeAddress();

            Apply(new MemberAddressChangedEvent(addressLineOne, addressLineTwo, town, county, country, postalCode));
        }

        public void LoanBook(string title, string isbn, string author, string category, int rentalLimit)
        {
            var book = new Book(new BookTitle(title, isbn, author, category), rentalLimit);

            canLoanBook(book);

            Apply(new MemberLoanedBookEvent(title, isbn, author, category, rentalLimit, 
                DateTime.Now, DateTime.Now.AddDays(rentalLimit)));
        }

        public void ReturnBook(string title, string isbn, string author, string category, int rentalLimit, DateTime expectedReturnDate)
        {
            var book = new Book(new BookTitle(title, isbn, author, category), rentalLimit);

            canReturnBook(book);

            Apply(new MemberReturnedBookEvent(title, isbn, author, category, rentalLimit, expectedReturnDate, DateTime.Now));
        }

        private void canChangeName()
        {
            if (areBooksOnLoan())
                throw new CannotChangeMemberNameException("Cannot change name while books are out on loan");
        }

        private void canChangeAddress()
        {
            if (areBooksOnLoan())
                throw new CannotChangeMemberAddressException("Cannot change address while books are out on loan");
        }

        private void canLoanBook(Book book)
        {
            if (book.IsOnLoan())
                throw new CannotLoanBookException("The book is already on loan");

            isMemberSuspended();
        }

        private void canReturnBook(Book book)
        {
            if (!_loanedBooks.Contains(book))
                throw new CannotReturnBookException("Memer does not have this book out on loan");
        }

        private void isMemberSuspended()
        {
            if (_isMemberSuspended)
                throw new MemberIsSuspendedException("The current member is suspended");
        }

        private bool areBooksOnLoan()
        {
            return _loanedBooks.Count > 0;
        }

        private void registerEvents()
        {
            RegisterEvent<MemberCreatedEvent> (onMemberCreatedEvent);
            RegisterEvent<MemberNameChangedEvent>(onMemberNameChangedEvent);
            RegisterEvent<MemberAddressChangedEvent>(onMemberAddressChangedEvent);
            RegisterEvent<MemberLoanedBookEvent>(onMemberLoanedBookEvent);
            RegisterEvent<MemberReturnedBookEvent>(onMemberReturnedBookEvent);
        }

        private void onMemberCreatedEvent(MemberCreatedEvent memberCreatedEvent)
        {
            _memberId = memberCreatedEvent.MemberId;
            _firstName = memberCreatedEvent.FirstName;
            _lastName = memberCreatedEvent.LastName;
            _address = 
                new Address(memberCreatedEvent.AddressLineOne, memberCreatedEvent.AddressLineTwo, 
                memberCreatedEvent.Town, memberCreatedEvent.County, memberCreatedEvent.Country, memberCreatedEvent.PostalCode);
            _dateOfBirth = memberCreatedEvent.DateOfBirth;
        }

        private void onMemberNameChangedEvent(MemberNameChangedEvent memberNameChangedEvent)
        {
            _firstName = memberNameChangedEvent.FirstName;
            _lastName = memberNameChangedEvent.LastName;
        }

        private void onMemberAddressChangedEvent(MemberAddressChangedEvent memberAddressChangedEvent)
        {
            _address = 
                new Address(memberAddressChangedEvent.AddressLineOne, memberAddressChangedEvent.AddressLineTwo,
                    memberAddressChangedEvent.Town, memberAddressChangedEvent.County, memberAddressChangedEvent.Country,
                    memberAddressChangedEvent.PostalCode);
        }

        private void onMemberLoanedBookEvent(MemberLoanedBookEvent memberLoanedBookEvent)
        {
            Book book =
                new Book(
                new BookTitle(memberLoanedBookEvent.Title, memberLoanedBookEvent.Isbn,
                    memberLoanedBookEvent.Author, memberLoanedBookEvent.Category), memberLoanedBookEvent.RentalLimit);

            book.Loan();
            _loanedBooks.Add(book);
        }

        private void onMemberReturnedBookEvent(MemberReturnedBookEvent memberReturnedBookEvent)
        {
              Book book =
                new Book(
                new BookTitle(memberReturnedBookEvent.Title, memberReturnedBookEvent.Isbn,
                    memberReturnedBookEvent.Author, memberReturnedBookEvent.Category), memberReturnedBookEvent.RentalLimit);

              book.Return();
              _loanedBooks.Remove(book);
        }

        public override string ToString()
        {
            string firstName = String.IsNullOrEmpty(_firstName) ? String.Empty : _firstName;
            string lastName = String.IsNullOrEmpty(_lastName) ? String.Empty : _lastName;

            return String.Concat(firstName, " ", lastName);
        }
    }
}
