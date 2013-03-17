using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Members
{
    public class CannotReturnBookException : Exception
    {
        public CannotReturnBookException(string message)
            : base(message)
        {

        }
    }
}
