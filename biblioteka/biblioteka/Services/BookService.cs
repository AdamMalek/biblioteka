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

        public BookService(LibraryContext db)
        {
            _db = db;
        }
        public void AddBook(Book book)
        {
            if (book == null) return;
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void ChangeBookState(int id, EBookState newState)
        {
            var book = GetBookById(id);
            if (book != null)
            {
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
            return _db.Books.Include("Author").Include("Category").ToList();
        }

        public Book GetBookById(int id)
        {
            return _db.Books.Include("Author").Include("Category").FirstOrDefault(x => x.Id == id);
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