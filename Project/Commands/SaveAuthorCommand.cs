namespace Project.Commands;

public class SaveAuthorCommand
{
    public Guid? Id { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }
}