using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteka.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByPESEL(string PESEL);
        void AddUser(string firstName, string lastName,string PESEL);
        void EditUser(int id, string firstName = "", string lastName = "", string PESEL="");
        void RemoveUser(int id);
    }
}
