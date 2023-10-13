using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Queries.Handlers;

public class GetMultipleBooksHandler
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMultipleBooksHandler(IDbContext dbContext, IMapper mapper)
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
            (query.AuthorId== null || query.AuthorId == x.AuthorId) &&
            (string.IsNullOrEmpty(query.Title) || x.Title.ToLower() == query.Title.ToLower()) &&
            (query.PublishDate == null || x.PublishDate == query.PublishDate) &&
            (query.PageCount == null || x.PageCount == query.PageCount) &&
            (query.Genre == null || x.GenreId == genreId) &&
            (query.Active == null || x.Active == query.Active)).ToList();

        var bookResults = new List<BookDto>();

        foreach (var book in filteredBooks)
        {
            var bookVo = _mapper.Map<BookDto>(book);

            var author = _dbContext.Authors.FirstOrDefault(x => x.Id == book.AuthorId);

            if(author is null)
            {
                throw new InvalidOperationException($"Author id= {book.AuthorId} did not find");
            }

            bookVo.AuthorFirstName = author.FirstName;
            bookVo.AuthorSurname = author.Surname;

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
            throw new InvalidOperationException($"Genre id={id} did not find");
        }
    }
}
