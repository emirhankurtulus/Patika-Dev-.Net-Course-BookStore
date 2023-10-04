using AutoMapper;
using Project.DBOperations;
using Project.DTO;

namespace Project.Queries.Handlers;

public class GetAuthorHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AuthorDto Handle(Guid id)
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == id);

        var result = _mapper.Map<AuthorDto>(author);

        return result;
    }
}