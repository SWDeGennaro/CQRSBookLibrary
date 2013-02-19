using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Books
{
    public class CannotChangeBookTitleException : Exception
    {
        public CannotChangeBookTitleException(string message) : base(message)
        {
        }
    }
}
