using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using biblioteka.DAL;
using biblioteka.Models;
using biblioteka.Services.Interfaces;
using biblioteka.ViewModels;

namespace biblioteka.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(IBookService bookService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
        }

        // GET: Books
        public ActionResult Index(string FilterText = "",int SelectedCategoryId = -1)
        {
            IEnumerable<Book> books;
            if (SelectedCategoryId != -1)
            {
                var category = _categoryService.GetCategoryById(SelectedCategoryId);
                books = category?.Books;
            }
            else
            {
                books = _bookService.GetAllBooks();
            }
            books = books.Where(x => MatchFilter(FilterText, x));
            var booksVM = books.Select(x => CreateVM(x)).OrderBy(x=> x.Title).ToList();
            var categories = _categoryService.GetAllCategories().Select(x => new CategoryVM { CategoryName = x.CategoryName, Id = x.Id }).ToList();
            categories.Insert(0, new CategoryVM { Id = -1, CategoryName = "--------" });
            return View(new BookListVM
            {
                Books = booksVM,
                Categories = categories.OrderBy(x=> x.CategoryName).ToList()
            });
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var vm = new AddEditBookVM();
            vm.Book = new BookVM();
            vm.Categories = _categoryService.GetAllCategories().Select(x => new CategoryVM { Id = x.Id, CategoryName = x.CategoryName }).OrderBy(x=> x.CategoryName).ToList();
            vm.Categories.Insert(0, new CategoryVM { Id = -1, CategoryName = "Bez kategorii" });
            return View("CreateEdit", vm);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEditBookVM vm)
        {
            if (ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Author = vm.Book.Author,
                    ISBN = vm.Book.ISBN,
                    Price = vm.Book.Price,
                    Title = vm.Book.Title,
                    BookState = EBookState.Available,
                    Category = (vm.Book.Category.Id != -1) ? _categoryService.GetCategoryById(vm.Book.Category.Id) : null,
                    User = null
                };
                _bookService.AddBook(newBook);
                return RedirectToAction("Index");
            }
            return View("CreateEdit", vm);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _bookService.GetBookById(id.Value);
            if (book != null)
            {
                var vm = new AddEditBookVM();
                vm.Book = CreateVM(book);
                vm.Categories = _categoryService.GetAllCategories().Select(x => new CategoryVM { Id = x.Id, CategoryName = x.CategoryName }).OrderBy(x=> x.CategoryName).ToList();
                vm.Categories.Insert(0, new CategoryVM { Id = -1, CategoryName = "Bez kategorii" });
                return View("CreateEdit", vm);
            }
            return RedirectToAction("Index");
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddEditBookVM vm)
        {
            if (ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Author = vm.Book.Author,
                    ISBN = vm.Book.ISBN,
                    Price = vm.Book.Price,
                    Title = vm.Book.Title,
                    Category = (vm.Book.Category.Id != -1) ? _categoryService.GetCategoryById(vm.Book.Category.Id) : null
                };
                _bookService.ModifyBook(vm.Book.Id, newBook);
                return RedirectToAction("Index");
            }
            return View("CreateEdit", vm);
        }

        //// GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //// POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }
        private BookVM CreateVM(Book book)
        {
            var vm = new BookVM
            {
                Id = book.Id,
                Author = book.Author,
                BookState = book.BookState,
                ISBN = book.ISBN,
                Price = book.Price,
                Title = book.Title
            };

            if (book.Category != null)
            {
                vm.Category = new CategoryVM
                {
                    CategoryName = book.Category.CategoryName,
                    Id = book.Category.Id
                };
            }
            else
            {
                vm.Category = new CategoryVM
                {
                    CategoryName = "Bez kategorii",
                    Id = -1
                };
            }

            if (book.User != null)
            {
                vm.User = new UserVM
                {
                    Id = book.User.Id,
                    FullName = $"{book.User.FirstName} {book.User.LastName}"
                };
            }
            else
            {
                vm.User = new UserVM
                {
                    Id = -1,
                    FullName = ""
                };
            }

            return vm;
        }

        private bool MatchFilter(string filter, Book x)
        {
            if (filter == "") return true;
            if (x.Author.ToLower().Contains(filter.ToLower())) return true;
            if (x.Title.ToLower().Contains(filter.ToLower())) return true;
            return false;
        }
    }
}
