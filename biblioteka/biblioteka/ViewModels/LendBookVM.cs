using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class LendBookVM
    {
        public BookVM Book { get; set; }
        public IEnumerable<UserVM> Users { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}