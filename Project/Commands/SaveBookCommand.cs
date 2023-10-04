namespace Project.Commands.Books;

public class SaveBookCommand
{
    public Guid? Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; }

    public string Genre { get; set; }

    public int PageCount { get; set; }

    public DateOnly PublishDate { get; set; }
}