using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biblioteka.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public EBookState BookState { get; set; }
        public virtual ICollection<Author> Author { get; set; }
        public virtual Category Category { get; set; }

        public Book()
        {
            Author = new List<Author>();
        }
    }

    public enum EBookState
    {
        [Display(Name ="Dostępna")]
        Available = 0,
        [Display(Name = "Wypożyczona")]
        Borrowed = 1
    };
}