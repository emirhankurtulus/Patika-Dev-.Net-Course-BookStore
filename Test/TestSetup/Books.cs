using Project.DBOperations;
using Project.Entities;

namespace Test.TestSetup;

public static class Books
{
    public static void AddBooks(this IDbContext context)
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
}