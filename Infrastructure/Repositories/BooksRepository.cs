using Infrastructure.Entities;
using Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BooksRepository
    {
        public ApplicationDbContext Context { get; set; }
        public BooksRepository(ApplicationDbContext context) {  Context = context; }

        public List<Book> GetBooks()
        {
            return Context.Books.ToList();
        }
    }
}
