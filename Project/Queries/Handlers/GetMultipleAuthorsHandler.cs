using AutoMapper;
using Project.DBOperations;
using Project.DTO;

namespace Project.Queries.Handlers;

public class GetMultipleAuthorsHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMultipleAuthorsHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IList<AuthorDto> Handle(GetMultipleAuthorsQuery query)
    {
        var filteredAuthors = _dbContext.Authors.Where(x =>
            (string.IsNullOrEmpty(query.FirstName) || x.FirstName.ToLower() == query.FirstName.ToLower()) &&
            (string.IsNullOrEmpty(query.Surname) || x.Surname.ToLower() == query.Surname.ToLower()) &&
            (query.BirthDate == null || x.BirthDate == query.BirthDate)).ToList();

        return filteredAuthors.Select(author => _mapper.Map<AuthorDto>(author)).ToList();
    }
}