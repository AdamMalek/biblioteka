using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class AddEditBookVM
    {
        public BookVM Book { get; set; }
        public List<AuthorVM> Authors { get; set; }
        public List<Category> Categories { get; set; }
    }
}