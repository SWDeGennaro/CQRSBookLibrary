using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Books
{
    public class CannotLoanBookException : Exception
    {
        public CannotLoanBookException(string message) : base(message)
        {

        }
    }
}
