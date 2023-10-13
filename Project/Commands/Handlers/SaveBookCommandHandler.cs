using AutoMapper;
using Project.Commands.Books;
using Project.DBOperations;
using Project.Entities;

namespace Project.Commands.Handlers;

public class SaveBookCommandHandler
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaveBookCommandHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(SaveBookCommand command)
    {
        if (command.Id is not null)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == command.Id);

            if (book is null)
            {
                throw new InvalidOperationException($"Book id={command.Id} did not find");
            }

            var author = _dbContext.Authors.FirstOrDefault(x => x.Id == command.AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException($"Author id= {command.AuthorId} did not find");
            }

            book.PublishDate = command.PublishDate;
            book.GenreId = GetGenreIdFromName(command.Genre);
            book.PageCount = command.PageCount;
            book.Title = command.Title;
            book.Active = command.Active;

            _dbContext.Books.Update(book);

            _dbContext.SaveChanges();
        }
        else
        {
            var author = _dbContext.Authors.FirstOrDefault(x => x.Id == command.AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException($"Author id= {command.AuthorId} did not find");
            }

            var createdEntity = _mapper.Map<Book>(command);

            createdEntity.GenreId = GetGenreIdFromName(command.Genre);

            _dbContext.Books.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }

    private Guid GetGenreIdFromName(string genreName)
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Name.ToLower() == genreName.ToLower());

        if (genre != null)
        {
            return genre.Id;
        }
        else
        {
            throw new InvalidOperationException($"{genreName} Genre did not find");
        }
    }
}