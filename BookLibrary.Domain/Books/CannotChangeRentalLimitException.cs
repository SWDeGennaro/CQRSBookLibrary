using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Books
{
    public class CannotChangeRentalLimitException : Exception
    {
        public CannotChangeRentalLimitException(string message) : base(message)
        {

        }
    }
}
