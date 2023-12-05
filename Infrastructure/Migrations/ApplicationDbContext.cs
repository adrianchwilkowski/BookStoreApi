using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookInfo> BookInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .IsRequired();
            builder.Entity<Book>()
                .HasMany(e => e.BookInfoList)
                .WithOne(e => e.Book)
                .HasForeignKey(e => e.BookId)
                .IsRequired();
            builder.Entity<OrderedItem>()
                .HasOne(e => e.BookInfo)
                .WithMany(e => e.Items)
                .IsRequired();
        }
    }
}
