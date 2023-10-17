using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());

        if (!context.Books.Any())
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "Test3",
                    PageCount = 123,
                    AuthorId = new Guid("717a2028-601d-4485-ab48-1ee95f124ae3"),
                    GenreId = new Guid("48278b65-4abe-45ab-9ba1-61e296bec4bf"),
                    PublishDate = new DateOnly(2002, 03, 13),
                    Active = true,
                },

                new Book
                {
                    Title = "Test",
                    PageCount = 325,
                    AuthorId = new Guid("bf99f27e-5324-4627-8b8d-50c362888e53"),
                    GenreId = new Guid("8f6cbf9f-6536-4618-881e-5f08b36ffe78"),
                    PublishDate = new DateOnly(2022, 03, 12),
                    Active = true,
                },

                new Book
                {
                    Title = "Test2",
                    PageCount = 453,
                    AuthorId = new Guid("bf99f27e-5324-4627-8b8d-50c362888e53"),
                    GenreId = new Guid("f9324d2b-d5e8-430b-89f7-50c58777ce80"),
                    PublishDate = new DateOnly(2023, 03, 13),
                    Active = true,
                });
        }

        if (!context.Genres.Any())
        {
            context.Genres.AddRange(
                new Genre
                {
                    Id = new Guid("48278b65-4abe-45ab-9ba1-61e296bec4bf"),
                    Name = "Adventure",
                    Active = true,
                },
                new Genre
                {
                    Id = new Guid("8f6cbf9f-6536-4618-881e-5f08b36ffe78"),
                    Name = "Classics",
                    Active = true,
                },
                new Genre
                {
                    Id = new Guid("f9324d2b-d5e8-430b-89f7-50c58777ce80"),
                    Name = "Crime",
                    Active = true,
                },
                new Genre
                {
                    Id = new Guid("34de5cd6-8e15-4ce3-970c-2aa2d7e4b46b"),
                    Name = "Fantasy",
                    Active = true,
                });
        }

        if (!context.Authors.Any())
        {
            context.Authors.AddRange(
               new Author
               {
                   Id = new Guid("717a2028-601d-4485-ab48-1ee95f124ae3"),
                   FirstName = "Emirhan",
                   Surname = "Kurtuluş",
                   BirthDate = new DateOnly(2000, 02, 06)
               },
               new Author
               {
                   Id = new Guid("bf99f27e-5324-4627-8b8d-50c362888e53"),
                   FirstName = "Hasanhan",
                   Surname = "Yörük",
                   BirthDate = new DateOnly(1999, 12, 05)
               },
               new Author
               {
                   Id = new Guid("ad12094e-2af6-45e6-922a-14356b7ae657"),
                   FirstName = "Cihad",
                   Surname = "Koşar",
                   BirthDate = new DateOnly(2000, 01, 06)
               });
        }

        context.SaveChanges();
    }
}