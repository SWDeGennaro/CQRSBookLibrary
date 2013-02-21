using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class BookLoanedEvent : DomainEvent
    {
        public string FirstName{ get; private set; }

        public string LastName { get; private set; }

        public BookLoanedEvent(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
