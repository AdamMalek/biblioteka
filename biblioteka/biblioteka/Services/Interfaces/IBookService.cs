using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteka.Services.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
    }
}
