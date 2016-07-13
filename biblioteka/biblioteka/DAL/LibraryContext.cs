using biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace biblioteka.DAL
{
    public class LibraryContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public LibraryContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LibraryContext>());
        }
    }
}