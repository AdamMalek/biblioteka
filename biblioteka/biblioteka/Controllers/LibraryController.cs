using biblioteka.Models;
using biblioteka.Services.Interfaces;
using biblioteka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace biblioteka.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public LibraryController(IUserService userService, IBookService bookService, ICategoryService categoryService)
        {
            _userService = userService;
            _bookService = bookService;
            _categoryService = categoryService;
        }
        // GET: Library
        public ActionResult Index(string FilterText = "", int SelectedCategoryId = -1)
        {
            ViewBag.Header = "Wszystkie książki:";
            var vm = CreateVM(FilterText, SelectedCategoryId);
            return View("BookList",vm);
        }
        public ActionResult Available(string FilterText = "", int SelectedCategoryId = -1)
        {
            ViewBag.Header = "Dostępne książki:";
            var vm = CreateVM(FilterText, SelectedCategoryId,false,true);
            return View("BookList", vm);
        }
        public ActionResult Borrowed(string FilterText = "", int SelectedCategoryId = -1)
        {
            ViewBag.Header = "Wypożyczone książki:";
            var vm = CreateVM(FilterText, SelectedCategoryId, false, false);
            return View("BookList", vm);
        }
        
        public ActionResult LendBook(int id)
        {
            Book book = _bookService.GetBookById(id);
            var bookVM = CreateBookVM(book);
            var usersVMs = _userService.GetAllUsers().Select(x => new UserVM { FullName = $"{x.FirstName} {x.LastName}", Id = x.Id, PESEL = x.PESEL }).OrderBy(x=> x.FullName);
            return View(new LendBookVM
            {
                Book = bookVM,
                Users = usersVMs,
                BookId = bookVM.Id,
                UserId = (usersVMs.FirstOrDefault() != null) ? usersVMs.First().Id : -1
            });
        }

        [HttpPost]
        public ActionResult LendBook(int BookId, int UserId)
        {
            _bookService.ChangeBookState(BookId, EBookState.Borrowed, UserId);
            return RedirectToAction("Details", "Users", new { id = UserId });
        }

        public ActionResult ReturnBook(int id)
        {
            _bookService.ChangeBookState(id, EBookState.Available);
            return RedirectToAction("Index");
        }

        private BookVM CreateBookVM(Book book)
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

        private BookListVM CreateVM(string FilterText,int SelectedCategoryId,bool all=true,bool available=true)
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
            if (!all)
            {
                if (available)
                {
                    books = books.Where(x => x.BookState == EBookState.Available);
                }
                else
                {
                    books = books.Where(x => x.BookState == EBookState.Borrowed);
                }
            }
            books = books.Where(x => MatchFilter(FilterText, x));
            var booksVM = books.Select(x => CreateBookVM(x)).ToList();
            var categories = _categoryService.GetAllCategories().Select(x => new CategoryVM { CategoryName = x.CategoryName, Id = x.Id }).ToList();
            categories.Insert(0, new CategoryVM { Id = -1, CategoryName = "--------" });
            return new BookListVM
            {
                Books = booksVM,
                Categories = categories.OrderBy(x => x.CategoryName).ToList()
            };
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