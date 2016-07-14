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
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            var vm = new UserListVM { Users = users.Select(x => new UserVM { PESEL = x.PESEL, Id = x.Id, BooksBorrowedCount = x.BooksBorrowed.Count, FullName = $"{x.FirstName} {x.LastName}" }).OrderBy(z=> z.FullName) };
            return View(vm);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetUserById(id.Value);
            if (user != null)
            {
                UserDetailsVM vm = new UserDetailsVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PESEL = user.PESEL,
                    Books = user.BooksBorrowed.Select(
                        x => new BookVM
                        {
                            Author = x.Author,
                            BookState = x.BookState,
                            Id = x.Id,
                            ISBN = x.ISBN,
                            Title = x.Title,
                            Price = x.Price,
                            Category = new CategoryVM { CategoryName = x.Category?.CategoryName, Id = (x.Category != null ) ? x.Category.Id : -1 }
                        })
                        .ToList()
                };
                return View(vm);
            }
            return HttpNotFound();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View("CreateEdit",new AddEditUserVM());
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEditUserVM user)
        {
            if (_userService.GetUserByPESEL(user.PESEL) != null)
            {
                ModelState.AddModelError("PESEL", "Istnieje osoba z takim PESELem");
            }
            if (ModelState.IsValid)
            {
                _userService.AddUser(user.FirstName, user.LastName, user.PESEL);
                return RedirectToAction("Index");
            }
            return View("CreateEdit", user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetUserById(id.Value);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var vm = new AddEditUserVM { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, PESEL = user.PESEL };
            return View("CreateEdit", vm);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddEditUserVM user)
        {
            var peselusr = _userService.GetUserByPESEL(user.PESEL);
            var idusr = _userService.GetUserById(user.Id);
            if (peselusr != null)
            {
                if (peselusr != idusr)
                ModelState.AddModelError("PESEL", "Istnieje osoba z takim PESELem");
            }
            if (ModelState.IsValid)
            {
                _userService.EditUser(user.Id, user.FirstName, user.LastName, user.PESEL);
                return RedirectToAction("Index");
            }
            return View("CreateEdit", user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetUserById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userService.RemoveUser(id);
            return RedirectToAction("Index");
        }
    }
}
