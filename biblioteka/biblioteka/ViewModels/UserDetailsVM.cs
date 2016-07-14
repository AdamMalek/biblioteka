using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class UserDetailsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public List<BookVM> Books { get; set; }
    }
}