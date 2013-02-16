using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Books
{
    public class BookTitle
    {
        public string Title { get; private set; }

        public string Isbn { get; private set; }

        public string Author { get; private set; }

        public string Category { get; private set; }

        public BookTitle(string title, string isbn, string author, string category)
        {
            Title = title;
            Isbn = isbn;
            Author = author;
            Category = category;
        }
    }
}
