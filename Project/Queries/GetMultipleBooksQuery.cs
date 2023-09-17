namespace Project.Queries;

public class GetMultipleBooksQuery
{
    public string? Title { get; set; }

    public string? Genre { get; set; }

    public int? PageCount { get; set; }

    public DateOnly? PublishDate { get; set; }
}
