using Project.DBOperations;

namespace Project.Commands.Handlers;

public class DeleteAuthorCommandHandler
{
    private readonly BookStoreDbContext _dbContext;


    public DeleteAuthorCommandHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(Guid id)
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == id);

        if (author is null)
        {
            throw new InvalidOperationException($"Author id= {id} did not find");
        }
        else
        {
            var books = _dbContext.Books.Where(x => x.AuthorId == id).ToArray();

            if (books is null || books.Length > 0)
            {
                throw new InvalidOperationException($"This Author id={id} is used in any book(s)");
            }

            _dbContext.Authors.Remove(author);

            _dbContext.SaveChanges();
        }
    }
}