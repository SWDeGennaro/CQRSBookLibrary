using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Domain.Books
{
    public class BookTitle
    {
        public readonly string Title;
        public readonly string Isbn;
        public readonly string Author;
        public readonly string Category;

        public BookTitle(string title, string isbn, string author, string category)
        {
            Title = title;
            Isbn = isbn;
            Author = author;
            Category = category;
        }
    }
}
