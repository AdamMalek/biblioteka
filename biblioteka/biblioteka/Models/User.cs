using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace biblioteka.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string PESEL { get; set; }
        public virtual ICollection<Book> BooksBorrowed { get; set; }

        public User()
        {
            BooksBorrowed = new List<Book>();
        }
    }
}