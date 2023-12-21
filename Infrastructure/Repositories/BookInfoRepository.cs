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
    public interface IBookInfoRepository
    {
        Task<List<BookInfo>> GetBookInfoList();
        Task<List<BookInfo>> GetByBookId(Guid bookId);
        Task Create(BookInfo book);
    }
    public class BookInfoRepository : IBookInfoRepository
    {
        public ApplicationDbContext Context { get; set; }
        public BookInfoRepository(ApplicationDbContext context) { Context = context; }

        public async Task<List<BookInfo>> GetBookInfoList()
        {
            return await Context.BookInfo
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<BookInfo>> GetByBookId(Guid bookId)
        {
            try
            {
                return await Context.BookInfo
                .Where(x => x.BookId == bookId)
                .ToListAsync();
            }
            catch (Exception) { throw new NotFoundException("Bookinfo with given ID doesn't exist."); }
        }
        public async Task Create(BookInfo bookInfo)
        {
            Context.Attach(bookInfo.Book);
            Context.BookInfo
                .Add(bookInfo);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AlreadyExistsException("Bookinfo with given ID already exists.");
            }
        }
    }
}
