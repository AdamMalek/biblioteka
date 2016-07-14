using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biblioteka.ViewModels
{
    public class AddEditUserVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Imię")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "PESEL")]
        [MinLength(11)]
        [MaxLength(11)]
        public string PESEL { get; set; }
    }
}