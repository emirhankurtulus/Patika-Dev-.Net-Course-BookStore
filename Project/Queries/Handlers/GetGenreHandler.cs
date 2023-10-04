using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Entities;

namespace Project.Queries.Handlers;

public class GetGenreHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GenreDto Handle(Guid id)
    {
        var genre = _dbContext.Genres.FirstOrDefault(g => g.Id == id);

        var result = _mapper.Map<GenreDto>(genre);

        return result;
    }
}