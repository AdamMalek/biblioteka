using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class BookListVM
    {
        public List<BookVM> Books { get; set; }
        [Display(Name = "Kategoria")]
        public List<CategoryVM> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public string FilterText { get; set; }
    }
}