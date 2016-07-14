using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteka.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        void AddCategory(string category);
        void EditCategoryName(int id, string name);
        void RemoveCategory(int id);
    }
}
