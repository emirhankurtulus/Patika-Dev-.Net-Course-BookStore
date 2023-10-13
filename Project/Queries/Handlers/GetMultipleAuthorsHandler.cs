using AutoMapper;
using Project.DBOperations;
using Project.DTO;

namespace Project.Queries.Handlers;

public class GetMultipleAuthorsHandler
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetMultipleAuthorsHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IList<AuthorDto> Handle(GetMultipleAuthorsQuery query)
    {
        var filteredAuthors = _context.Authors.Where(x =>
            (string.IsNullOrEmpty(query.FirstName) || x.FirstName.ToLower() == query.FirstName.ToLower()) &&
            (string.IsNullOrEmpty(query.Surname) || x.Surname.ToLower() == query.Surname.ToLower()) &&
            (query.BirthDate == null || x.BirthDate == query.BirthDate) &&
            (query.Active == null || x.Active == query.Active)).ToList();

        return filteredAuthors.Select(author => _mapper.Map<AuthorDto>(author)).ToList();
    }
}