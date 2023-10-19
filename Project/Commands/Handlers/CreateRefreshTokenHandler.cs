using Project.DBOperations;
using Project.DTO;
using Project.Services;

namespace Project.Commands.Handlers;

public class CreateRefreshTokenHandler
{
    private readonly IDbContext _dbContext;
    private readonly IAuthenticationService _authenticationService;

    public CreateRefreshTokenHandler(
        IDbContext dbContext,
        IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
    }

    public TokenDto Handle(string token)
    {
        var userId = _authenticationService.GetIdByToken(token);

        var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId && x.RefreshTokenExpireDate > DateTime.Now);

        if (user is null)
        {
            throw new UnauthorizedAccessException("RefreshToken did not find.");
        }

        var newToken = _authenticationService.CreateAccesToken(user.Id);

        user.RefreshToken = newToken.RefreshToken;

        user.RefreshTokenExpireDate = newToken.Expiration.AddMinutes(3);

        _dbContext.SaveChanges();

        return newToken;
    }
}