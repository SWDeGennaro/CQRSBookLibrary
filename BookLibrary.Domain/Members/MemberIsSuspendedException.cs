using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Members
{
    public class MemberIsSuspendedException : Exception
    {
        public MemberIsSuspendedException(string message)
            : base(message)
        {

        }
    }
}
