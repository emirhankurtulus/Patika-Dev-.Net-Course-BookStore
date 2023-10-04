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
            (string.IsNullOrEmpty(query.Name) || x.Name == query.Name) &&
            (query.Active == null || x.Active == query.Active)).ToList();

        var result = new List<GenreDto>();

        foreach (var genre in filteredGenres)
        {
            var genreVo = _mapper.Map<GenreDto>(genre);

            result.Add(genreVo);
        }

        return result;
    }
}