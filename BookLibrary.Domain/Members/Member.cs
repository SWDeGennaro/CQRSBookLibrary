using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;

namespace BookLibrary.Domain.Members
{
    public class Member : BaseAggregateRoot<IDomainEvent>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Member(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            string firstName = String.IsNullOrEmpty(FirstName) ? String.Empty : FirstName;
            string lastName = String.IsNullOrEmpty(LastName) ? String.Empty : LastName;

            return String.Concat(firstName, " ", lastName);
        }
    }
}
