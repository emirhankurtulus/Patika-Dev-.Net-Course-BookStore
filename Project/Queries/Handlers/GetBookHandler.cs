using Project.DBOperations;
using Project.DTO;
using Project.Enums;

namespace Project.Queries.Handlers;

public class GetBookHandler
{
    private readonly BookDBContext _dbContext;

    public GetBookHandler(BookDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookDto Handle(Guid id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

        var bookResult = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Value,
            Genre = ((GenreEnum)book.GenreId).ToString(),
        };

        return bookResult;
    }
}