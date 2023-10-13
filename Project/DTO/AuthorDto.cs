namespace Project.DTO;

public class AuthorDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }

    public bool Active { get; set; }
}