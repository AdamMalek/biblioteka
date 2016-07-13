using biblioteka.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteka.Models;

namespace biblioteka.Services
{
    public class BookService : IBookService
    {
        public List<Book> GetAllBooks()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title="test", ISBN = "123" }
            };
        }
    }
}