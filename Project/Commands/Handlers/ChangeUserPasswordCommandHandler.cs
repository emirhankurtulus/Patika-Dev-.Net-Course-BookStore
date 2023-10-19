using Project.DBOperations;
using Project.DTO;
using Project.Services;

namespace Project.Commands.Handlers;

public class ChangeUserPasswordCommandHandler
{
    private readonly IDbContext _dbContext;
    private readonly IAuthenticationService _authenticationService;
    private readonly IPasswordHelper _passwordHelper;

    public ChangeUserPasswordCommandHandler(
        IDbContext dbContext,
        IAuthenticationService authenticationService,
        IPasswordHelper passwordHelper)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
        _passwordHelper = passwordHelper;
    }

    public void Handle(ChangeUserPasswordCommand command, string token)
    {
        var isActiveUser = _authenticationService.ValidateToken(command.Id, token);

        if (!isActiveUser)
        {
            throw new UnauthorizedAccessException("Token is invalid.");
        }

        var user = _dbContext.Users.FirstOrDefault(x => x.Id == command.Id);

        if (user is null)
        {
            throw new UnauthorizedAccessException("User did not find.");
        }

        if (!_passwordHelper.VerifyPassword(command.PreviousPassword, user.Password))
        {
            throw new UnauthorizedAccessException("Wrong Password.");
        }

        user.Password = _passwordHelper.HashPassword(command.NewPassword);

        _dbContext.SaveChanges();
    }
}