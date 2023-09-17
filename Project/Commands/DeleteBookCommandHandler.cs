using Project.DBOperations;

namespace Project.Commands;

public class DeleteBookCommandHandler
{
    private readonly BookDBContext _dbContext;

    public DeleteBookCommandHandler(BookDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(Guid Id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == Id);

        if (book is null)
        {
            throw new InvalidOperationException("Book did not find");
        }
        else
        {
            _dbContext.Books.Remove(book);

            _dbContext.SaveChanges();
        }
    }
}
