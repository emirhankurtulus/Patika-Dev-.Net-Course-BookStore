using Project.DTO;

namespace Project.Services;

public interface IAuthenticationService
{
    TokenDto CreateAccesToken(Guid userId);

    bool ValidateToken(Guid userId, string token);

    Guid GetIdByToken(string token);
}