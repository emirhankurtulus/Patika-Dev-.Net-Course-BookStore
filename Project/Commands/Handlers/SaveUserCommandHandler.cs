using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.DBOperations;
using Project.Entities;
using Project.Services;

namespace Project.Commands.Handlers;

public class SaveUserCommandHandler
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IAuthenticationService _authenticationService;

    public SaveUserCommandHandler(
        IDbContext dbContext,
        IMapper mapper,
        IPasswordHelper passwordHelper,
        IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordHelper = passwordHelper;
        _authenticationService = authenticationService;
    }

    public void Handle(SaveUserCommand command, string token)
    {
        if (command.Id is not null)
        {
            if (!_authenticationService.ValidateToken(command.Id.Value, token))
            {
                throw new InvalidOperationException($"user email={command.Email} did not find");
            };

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == command.Id) ?? throw new InvalidOperationException($"user email={command.Email} did not find");

            user.FirstName = command.FirstName;
            user.Surname = command.Surname;
            user.Email = command.Email;

            _dbContext.Users.Update(user);

            _dbContext.SaveChanges();
        }
        else
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == command.Email);

            if(user is not null)
            {
                throw new InvalidOperationException($"user email={command.Email} already exist");
            }
            
            var createdEntity = _mapper.Map<User>(command);

            var password = _passwordHelper.HashPassword(command.Password);

            createdEntity.Password = password;

            createdEntity.RefreshToken = "";

            _dbContext.Users.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }
}