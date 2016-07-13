using biblioteka.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteka.Models;
using biblioteka.DAL;

namespace biblioteka.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _db;

        public UserService(LibraryContext db)
        {
            _db = db;
        }
        public void AddUser(string firstName, string lastName,string PESEL)
        {
            if (GetUserByPESEL(PESEL) == null)
            {
                _db.Users.Add(new User { FirstName = firstName, LastName = lastName,PESEL = PESEL });
            }
        }

        public void EditUser(int id, string firstName = "", string lastName = "", string PESEL="")
        {
            var user = GetUserById(id);
            if (user != null)
            {
                if (firstName != "")
                {
                    user.FirstName = firstName;
                }
                if (lastName != "")
                {
                    user.LastName = lastName;
                }
                if (PESEL != "" && GetUserByPESEL(PESEL) == null)
                {
                    user.PESEL = PESEL;
                }
                _db.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users;
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByPESEL(string PESEL)
        {
            return _db.Users.FirstOrDefault(x => x.PESEL == PESEL);
        }

        public void RemoveUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
    }
}