using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Members
{
    public class CannotChangeMemberAddressException : Exception
    {
        public CannotChangeMemberAddressException(string message)
            : base(message)
        {
        }
    }
}
