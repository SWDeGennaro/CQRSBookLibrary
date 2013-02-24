using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.Events;

namespace BookLibrary.Domain.Members
{
    public class Member : BaseAggregateRoot<IDomainEvent>
    {
        public Guid MemberId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public string DateOfBirth { get; private set; }

        public Member()
        {
            MemberId = Guid.NewGuid();
            FirstName = String.Empty;
            LastName = String.Empty;
            DateOfBirth = String.Empty;
            registerEvents();
        }

        public Member(string firstName, string lastName, Address address, string dateOfBirth)
        {
            Apply(new MemberCreatedEvent(Guid.NewGuid(), firstName, lastName, address.AddressLineOne,
                address.AddressLineTwo, address.Town, address.County, address.Country, address.PostalCode, dateOfBirth));
        }

        private void registerEvents()
        {
            RegisterEvent<MemberCreatedEvent> (onMemberCreatedEvent);
        }

        private void onMemberCreatedEvent(MemberCreatedEvent memberCreatedEvent)
        {
            MemberId = memberCreatedEvent.MemberId;
            FirstName = memberCreatedEvent.FirstName;
            LastName = memberCreatedEvent.LastName;
            Address = 
                new Address(memberCreatedEvent.AddressLineOne, memberCreatedEvent.AddressLineTwo, 
                memberCreatedEvent.Town, memberCreatedEvent.County, memberCreatedEvent.Country, memberCreatedEvent.PostalCode);
            DateOfBirth = memberCreatedEvent.DateOfBirth;
        }

        public override string ToString()
        {
            string firstName = String.IsNullOrEmpty(FirstName) ? String.Empty : FirstName;
            string lastName = String.IsNullOrEmpty(LastName) ? String.Empty : LastName;

            return String.Concat(firstName, " ", lastName);
        }
    }
}
