using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetById(Guid id);
        Task<Book> GetByTitle(string title);
        Task Create(Book book);
    }
    public class BooksRepository : IBooksRepository
    {
        public ApplicationDbContext Context { get; set; }
        public BooksRepository(ApplicationDbContext context) {  Context = context; }

        public async Task<List<Book>> GetBooks()
        {
            return await Context.Books
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Book> GetById(Guid id)
        {
            try
            {
                return await Context.Books
                .Where(x => x.Id == id)
                .FirstAsync();
            }
            catch (Exception) { throw new NotFoundException("Book with given ID doesn't exist."); }
        }
        public async Task<Book> GetByTitle(string title)
        {
            try
            {
                return await Context.Books
                .Where(x => x.Title == title)
                .FirstAsync();
            }
            catch (Exception) { throw new NotFoundException("Book with given title doesn't exist."); }
        }
        public async Task Create(Book book)
        {
            Context.Books
                .Add(book);
            try {
                await Context.SaveChangesAsync();
            } 
            catch (Exception) 
            {
                throw new AlreadyExistsException("Book with given ID already exists.");
            }
        }
    }
}
