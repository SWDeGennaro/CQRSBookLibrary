using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BookLibrary.Domain.Books;
using BookLibrary.Domain.Mementos;

namespace BookLibrary.Tests.Unit.Domain.Memento
{
    [TestFixture]
    public class BookMementoTests
    {
        private Book _book;
        private BookMemento _memento;

        [Given]
        public void Setup()
        {
            _book = new Book(new BookTitle("Oliver", "292929", "Charles Dickins", "Fiction"), 4);

            _memento = (BookMemento)_book.CreateMemento();
        }

        [Test]
        public void Create_book_memento_returns_valid_book_title_memento()
        {
            Assert.AreEqual(typeof(BookMemento), _memento.GetType());

            Assert.AreEqual("Oliver", _memento.Title);
            Assert.AreEqual("292929", _memento.Isbn);
            Assert.AreEqual("Charles Dickins", _memento.Author);
            Assert.AreEqual("Fiction", _memento.Category);
            Assert.AreEqual(4, _memento.RentalLimit);
        }

    }
}
