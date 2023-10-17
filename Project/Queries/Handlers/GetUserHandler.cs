using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Services;

namespace Project.Queries.Handlers;

public class GetUserHandler
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public GetUserHandler(
        IDbContext dbContext, 
        IMapper mapper, 
        IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public UserDto Handle(string token)
    {
        var userId = _authenticationService.GetIdByToken(token);

        var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

        var result = _mapper.Map<UserDto>(user);

        return result;
    }
}