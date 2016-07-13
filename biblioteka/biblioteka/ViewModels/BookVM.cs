using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public double Price { get; set; }
        public string ISBN { get; set; }
        public string CategoryName { get; set; }
        public EBookState BookState { get; set; }
    }
}