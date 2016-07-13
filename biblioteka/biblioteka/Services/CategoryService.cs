using biblioteka.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biblioteka.Models;
using biblioteka.DAL;

namespace biblioteka.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryContext _db;

        public CategoryService(LibraryContext db)
        {
            _db = db;
        }
        public void AddCategory(Category category)
        {
            var categoryByName = GetCategoryByName(category.CategoryName);
            if (categoryByName == null)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
        }

        public void EditCategoryName(int id, string name)
        {
            var catID = GetCategoryById(id);
            var catName = GetCategoryByName(name);
            if (catID != null && catName == null)
            {
                catID.CategoryName = name;
                _db.SaveChanges();
            }

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _db.Categories.FirstOrDefault(x => x.CategoryName == name);
        }

        public void RemoveCategory(int id)
        {
            var cat = GetCategoryById(id);
            removeCategory(cat);
        }

        public void RemoveCategory(string name)
        {
            var cat = GetCategoryByName(name);
            removeCategory(cat);
        }

        private void removeCategory(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }
}