using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events
{
    [Serializable]
    public class MemberCreatedEvent : DomainEvent
    {
        public Guid MemberId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string AddressLineOne { get; private set; }
        public string AddressLineTwo { get; private set; }
        public string Town { get; private set; }
        public string County { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string DateOfBirth { get; private set; }

        public MemberCreatedEvent(Guid memberId, string firstName, string lastName,
            string addressLineOne, string addressLineTwo, string town, string county, 
            string country, string postalCode, string dateOfBirth)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            Town = town;
            County = county;
            Country = country;
            PostalCode = postalCode;
            DateOfBirth = dateOfBirth;
        }
    }
}
