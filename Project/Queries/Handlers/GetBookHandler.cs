using AutoMapper;
using Project.DBOperations;
using Project.DTO;


namespace Project.Queries.Handlers;

public class GetBookHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDto Handle(Guid id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == book.AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException($"Author id={book.AuthorId} did not find");
        }

        var result = _mapper.Map<BookDto>(book);

        result.Genre = GetGenreName(book.GenreId);
        result.AuthorFirstName = author.FirstName;
        result.AuthorSurname = author.Surname;
        return result;
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