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
        Book GetBookById(int id);
        void AddBook(Book book);
        void ModifyBook(int id,Book book);
        void DeleteBook(int id);
        void ChangeBookState(int id, EBookState newState, int userId = -1);
    }
}
