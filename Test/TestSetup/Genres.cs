using Project.DBOperations;
using Project.Entities;

namespace Test.TestSetup;

public static class Genres
{
    public static void AddGenres(this IDbContext context)
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
}