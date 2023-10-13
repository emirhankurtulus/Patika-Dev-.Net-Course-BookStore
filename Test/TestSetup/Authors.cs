using Project.DBOperations;
using Project.Entities;

namespace Test.TestSetup;

public static class Authors
{
    public static void AddAuthors(this IDbContext context)
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
}