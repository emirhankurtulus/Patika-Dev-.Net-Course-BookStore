namespace Project.DTO;

public class BookDto
{
    public Guid? Id { get; set; }

    public string Genre { get; set; }

    public string Title { get; set; }

    public int PageCount { get; set; }

    public DateOnly PublishDate { get; set; }

    public bool Active { get; set; }
}
