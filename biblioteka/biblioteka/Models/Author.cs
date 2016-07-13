using System.Collections.Generic;

namespace biblioteka.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Book> BooksWritten { get; set; }
    }
}