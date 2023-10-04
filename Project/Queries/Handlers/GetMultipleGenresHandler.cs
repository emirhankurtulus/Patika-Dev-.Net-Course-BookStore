using AutoMapper;
using Project.DBOperations;
using Project.DTO;

namespace Project.Queries.Handlers;

public class GetMultipleGenresHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMultipleGenresHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IList<GenreDto> Handle(GetMultipleGenresQuery query)
    {
        var filteredGenres = _dbContext.Genres.Where(x=>
            (string.IsNullOrEmpty(query.Name) || x.Name.ToLower() == query.Name.ToLower()) &&
            (query.Active == null || x.Active == query.Active)).ToList();

        return filteredGenres.Select(genre => _mapper.Map<GenreDto>(genre)).ToList();
    }
}