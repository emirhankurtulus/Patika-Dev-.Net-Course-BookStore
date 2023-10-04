using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Enums;

namespace Project.Queries.Handlers;

public class GetMultipleBooksHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMultipleBooksHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public  IList<BookDto> Handle(GetMultipleBooksQuery query)
    {
        Guid? genreId = null;

        if (!string.IsNullOrEmpty(query.Genre))
        {
            var genre = _dbContext.Genres.FirstOrDefault(x => x.Name.ToLower() == query.Genre.ToLower());

            genreId = genre.Id;
        }

        var filteredBooks = _dbContext.Books.Where(x =>
            (string.IsNullOrEmpty(query.Title) || x.Title == query.Title) &&
            (query.PublishDate == null || x.PublishDate == query.PublishDate) &&
            (query.PageCount == null || x.PageCount == query.PageCount) &&
            (query.Genre == null || x.GenreId == genreId) &&
            (query.Active == null || x.Active == query.Active)).ToList();

        var bookResults = new List<BookDto>();

        foreach (var book in filteredBooks)
        {
            var bookVo = _mapper.Map<BookDto>(book);

            bookVo.Genre = GetGenreName(book.GenreId);

            bookResults.Add(bookVo);
        }

        return bookResults;
    }

    private string GetGenreName(Guid id)
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);

        if (genre is not null)
        {
            return genre.Name;
        }
        else
        {
            throw new InvalidOperationException("Genre did not find");
        }
    }
}
