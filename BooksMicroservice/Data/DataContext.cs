using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BooksApi.Models;

namespace BooksApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(builder =>
            {
                builder.Property(e => e.Isbn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

        }
    }
}