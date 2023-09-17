using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Enums;

namespace Project.Queries.Handlers;

public class GetMultipleBooksHandler
{
    private readonly BookDBContext _dbContext;
    private readonly IMapper _mapper;

    public GetMultipleBooksHandler(BookDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IList<BookDto> Handle(GetMultipleBooksQuery query)
    {
        GenreEnum genreEnumValue;

        int? genre = null;

        if (Enum.TryParse(query.Genre, out genreEnumValue))
        {
            genre = (int)genreEnumValue;
        }

        var filteredBooks = _dbContext.Books.Where(x =>
        (string.IsNullOrEmpty(query.Title) || x.Title == query.Title) && 
        (query.PublishDate == null || x.PublishDate == query.PublishDate) &&
        (query.PageCount == null || x.PageCount == query.PageCount)&&
        (query.Genre == null || x.GenreId == genre)).ToList();

        var bookResults = new List<BookDto>();

        foreach (var book in filteredBooks)
        {
            var bookvo = _mapper.Map<BookDto>(book);

            bookResults.Add(bookvo);
        }

        return bookResults;
    }
}
