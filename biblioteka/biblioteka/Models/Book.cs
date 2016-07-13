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
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Author { get; set; }
        public EBookState BookState { get; set; } = EBookState.Available;
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }

    public enum EBookState
    {
        [Display(Name ="Dostępna")]
        Available = 0,
        [Display(Name = "Wypożyczona")]
        Borrowed = 1
    };
}