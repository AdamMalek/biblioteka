using biblioteka.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteka.Models;
using System.Data.Entity;
using biblioteka.DAL;

namespace biblioteka.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _db;
        private readonly IUserService _userService;

        public BookService(LibraryContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }
        public void AddBook(Book book)
        {
            if (book == null) return;
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void ChangeBookState(int id, EBookState newState, int userId= -1)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                switch (newState)
                {
                    case EBookState.Available:
                        var oldUser = _userService.GetUserById(book.User.Id);
                        oldUser.BooksBorrowed.Remove(book);
                        book.User = null;
                        break;
                    case EBookState.Borrowed:
                        if (book.User != null)
                        {
                            book.User.BooksBorrowed.Remove(book);
                            var user = _userService.GetUserById(userId);
                            user.BooksBorrowed.Add(book);
                            book.User = user;                            
                        }
                        else
                        {
                            var user = _userService.GetUserById(userId);
                            user.BooksBorrowed.Add(book);
                            book.User = user;
                        }
                        break;
                    default:
                        break;
                }
                book.BookState = newState;
                _db.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var bookToRemove = GetBookById(id);
            if (bookToRemove != null)
            {
                _db.Books.Remove(bookToRemove);
                _db.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            return _db.Books.Include("User").Include("Category").ToList();
        }

        public Book GetBookById(int id)
        {
            return _db.Books.Include("User").Include("Category").FirstOrDefault(x => x.Id == id);
        }

        public void ModifyBook(int id, Book book)
        {
            var bookToEdit = GetBookById(id);
            if (bookToEdit != null)
            {
                bookToEdit.Author = book.Author;
                bookToEdit.Category = book.Category;
                bookToEdit.ISBN = book.ISBN;
                bookToEdit.Price = book.Price;
                bookToEdit.Title = book.Title;
                _db.SaveChanges();
            }
        }
    }
}