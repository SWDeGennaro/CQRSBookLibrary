using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Books
{
    [Serializable]
    public class BookRentalLimitChangedEvent : DomainEvent
    {
        public int RentalLimit { get; private set; }

        public BookRentalLimitChangedEvent(int rentalLimit)
        {
            RentalLimit = rentalLimit;
        }
    }
}
