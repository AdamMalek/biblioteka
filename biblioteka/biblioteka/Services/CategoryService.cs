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
            if (_db.Categories.Count() == 0)
            {
                Seed();
            }
        }

        private void Seed()
        {
            AddCategory("biografia/autobiografia/pamiętnik");
            AddCategory("fantastyka, fantasy, science fiction");
            AddCategory("historyczna");
            AddCategory("horror");
            AddCategory("klasyka");
            AddCategory("literatura młodzieżowa");
            AddCategory("literatura faktu");
            AddCategory("literatura współczesna");
            AddCategory("poezja");
            AddCategory("przygodowa");
            AddCategory("publicystyka literacka i eseje");
            AddCategory("Literatura obyczajowa i romans");
            AddCategory("satyra");
            AddCategory("thriller/sensacja/kryminał");
            AddCategory("utwór dramatyczny (dramat, komedia, tragedia)");
            AddCategory("astronomia, astrofizyka");
            AddCategory("biznes, finanse");
            AddCategory("encyklopedie i słowniki");
            AddCategory("ezoteryka, senniki, horoskopy");
            AddCategory("filozofia i etyka");
            AddCategory("flora i fauna");
            AddCategory("Literatura podróżnicza");
            AddCategory("informatyka i matematyka");
            AddCategory("historia");
            AddCategory("językoznawstwo, nauka o literaturze");
            AddCategory("nauki przyrodnicze (fizyka, chemia, biologia, itd.)");
            AddCategory("nauki społeczne (psychologia, socjologia, itd.)");
            AddCategory("popularnonaukowa");
            AddCategory("poradniki");
            AddCategory("poradniki dla rodziców");
            AddCategory("technika");
            AddCategory("albumy");
            AddCategory("czasopisma");
            AddCategory("film/kino/telewizja");
            AddCategory("hobby");
            AddCategory("komiksy");
            AddCategory("kulinaria, przepisy kulinarne");
            AddCategory("militaria, wojskowość");
            AddCategory("motoryzacja");
            AddCategory("muzyka");
            AddCategory("religia");
            AddCategory("rękodzieło");
            AddCategory("rozrywka");
            AddCategory("sport");
            AddCategory("sztuka");
            AddCategory("teatr");
            AddCategory("turystyka, mapy, atlasy");
            AddCategory("zdrowie, medycyna");
        }
        public void AddCategory(string category)
        {
            var categoryByName = GetCategoryByName(category);
            if (categoryByName == null)
            {
                _db.Categories.Add(new Category { CategoryName = category });
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