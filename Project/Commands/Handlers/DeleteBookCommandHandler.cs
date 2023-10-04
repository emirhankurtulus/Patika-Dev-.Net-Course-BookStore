using Project.DBOperations;

namespace Project.Commands.Handlers;

public class DeleteBookCommandHandler
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommandHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(Guid id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

        if (book is null)
        {
            throw new InvalidOperationException($"Book id= {id} did not find");
        }
        else
        {
            _dbContext.Books.Remove(book);

            _dbContext.SaveChanges();
        }
    }
}
