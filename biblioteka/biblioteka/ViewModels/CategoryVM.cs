using System.ComponentModel.DataAnnotations;

namespace biblioteka.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Display(Name ="Kategoria")]
        public string CategoryName { get; set; }
    }
}