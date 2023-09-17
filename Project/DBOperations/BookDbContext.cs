using Microsoft.EntityFrameworkCore;

namespace Project.DBOperations;

public class BookDBContext : DbContext
{
    public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
    {}

    public DbSet<Book> Books { get; set; }
}
