using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.DBOperations;

public class BookStoreDbContext : DbContext, IDbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {}

    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Genre> Genres { get; set; }

    public DbSet<User> Users { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}