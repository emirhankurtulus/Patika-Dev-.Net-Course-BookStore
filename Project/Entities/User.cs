namespace Project.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExpireDate { get; set; }
}