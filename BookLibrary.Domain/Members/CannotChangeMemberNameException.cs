using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Members
{
    public class CannotChangeMemberNameException : Exception
    {
        public CannotChangeMemberNameException(string message) : base(message)
        {
        }
    }
}
