using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Events.Members
{
    [Serializable]
    public class MemberReturnedBookEvent : DomainEvent
    {
        public readonly string Title;
        public readonly string Isbn;
        public readonly string Author;
        public readonly string Category;
        public readonly int RentalLimit;
        public readonly DateTime DateLoaned;
        public readonly DateTime ReturnDate;

        public MemberReturnedBookEvent(string title, string isbn, string author, string category, 
            int rentalLimit, DateTime dateLoaned, DateTime returnDate)
        {
            Title = title;
            Isbn = isbn;
            Author = author;
            Category = category;
            RentalLimit = rentalLimit;
            DateLoaned = dateLoaned;
            ReturnDate = returnDate;
        }
    }
}
