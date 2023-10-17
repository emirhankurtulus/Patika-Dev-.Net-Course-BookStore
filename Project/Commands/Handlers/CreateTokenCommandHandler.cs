using Project.DBOperations;
using Project.DTO;
using Project.Services;

namespace Project.Commands.Handlers;

public class CreateTokenCommandHandler
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IAuthenticationService _authenticationService;

    public CreateTokenCommandHandler(
        IDbContext dbContext, 
        IPasswordHelper passwordHelper,
        IAuthenticationService authenticationService)
    {
        _passwordHelper = passwordHelper;
        _dbContext = dbContext;
        _authenticationService = authenticationService;
    }

    public TokenDto Handle(CreateTokenCommand login)
    {
        var user = _dbContext.Users.FirstOrDefault(u=> u.Email == login.Email) ?? throw new InvalidOperationException($"User did not find");
        
        if (!_passwordHelper.VerifyPassword(login.Password, user.Password))
        {
            throw new InvalidOperationException($"User did not find");
        }

        var token = _authenticationService.CreateAccesToken(user.Id);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        
        _dbContext.SaveChanges();

        return token;
    }
}