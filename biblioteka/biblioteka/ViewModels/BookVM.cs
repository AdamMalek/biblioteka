using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Cena")]
        public double Price { get; set; }
        public string ISBN { get; set; }
        [Required]
        [Display(Name = "Kategoria")]
        public CategoryVM Category { get; set; }
        public UserVM User { get; set; }
        public EBookState BookState { get; set; }
    }
}