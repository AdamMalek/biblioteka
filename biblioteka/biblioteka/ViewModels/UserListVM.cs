using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class UserListVM
    {
        public IEnumerable<UserVM> Users { get; set; }
    }
}