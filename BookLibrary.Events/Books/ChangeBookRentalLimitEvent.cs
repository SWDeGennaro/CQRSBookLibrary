using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class ChangeBookRentalLimitEvent : DomainEvent
    {
        public int RentalLimit { get; private set; }

        public ChangeBookRentalLimitEvent(int rentalLimit)
        {
            RentalLimit = rentalLimit;
        }
    }
}
