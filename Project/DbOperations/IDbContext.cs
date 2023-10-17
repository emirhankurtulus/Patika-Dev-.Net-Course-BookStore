using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.DBOperations;

public interface IDbContext
{
    DbSet<Author> Authors { get; set; }

    DbSet<Book> Books { get; set; }

    DbSet<Genre> Genres { get; set; }

    DbSet<User> Users { get; set; }

    int SaveChanges();
}