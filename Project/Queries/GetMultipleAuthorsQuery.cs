namespace Project.Queries;

public class GetMultipleAuthorsQuery
{
    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public DateOnly? BirthDate { get; set; }

    public bool? Active { get; set; }
}