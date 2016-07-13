using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Book> Books { get; set; }

        public Category()
        {
            Books = new List<Book>();
        }
    }
}