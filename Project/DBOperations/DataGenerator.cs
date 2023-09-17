using Microsoft.EntityFrameworkCore;

namespace Project.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookDBContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book
                {
                    Title = "Test3",
                    GenreId = 2,
                    PageCount = 123,
                    PublishDate = new DateOnly(2002, 03, 13),
                },

                new Book
                {
                    Title = "Test",
                    GenreId = 1,
                    PageCount = 325,
                    PublishDate = new DateOnly(2022, 03, 12),
                },

                new Book
                {
                    Title = "Test2",
                    GenreId = 2,
                    PageCount = 453,
                    PublishDate = new DateOnly(2023, 03, 13),
                });

            context.SaveChanges();
        }
    }
}
