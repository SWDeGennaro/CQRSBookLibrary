using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Members
{
    [Serializable]
    public class MemberNameChangedEvent : DomainEvent
    {
        public readonly string FirstName;
        public readonly string LastName;

        public MemberNameChangedEvent(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;        
        }
    }
}
