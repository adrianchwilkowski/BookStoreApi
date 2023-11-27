using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedBooks> OrderedBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookInfo> BookInfo { get; set; }
    }
}
