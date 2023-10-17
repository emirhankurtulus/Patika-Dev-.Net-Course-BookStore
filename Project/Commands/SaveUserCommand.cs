namespace Project.Commands;

public class SaveUserCommand
{
    public Guid? Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }
}