using AutoMapper;
using Project.Commands.Books;
using Project.DBOperations;
using Project.DTO;
using Project.Entities;
using Project.Enums;

namespace Project.Commands.Handlers;

public class SaveBookCommandHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaveBookCommandHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(SaveBookCommand command)
    {
        GenreEnum genreEnumValue;
        int? genreId = null;

        if (Enum.TryParse(command.Genre, out genreEnumValue))
        {
            genreId = (int)genreEnumValue;
        }
        else
        {
            throw new InvalidOperationException("Genre did not find");
        }

        if (command.Id is not null)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == command.Id);

            if (book is null)
            {
                throw new InvalidOperationException("Book did not find");
            }

            book.PublishDate = command.PublishDate;
            book.GenreId = GetGenreIdFromName(command.Genre);
            book.PageCount = command.PageCount;
            book.Title = command.Title;

            _dbContext.Books.Update(book);

            _dbContext.SaveChanges();
        }
        else
        {
            var createdEntity = _mapper.Map<Book>(command);

            createdEntity.GenreId = GetGenreIdFromName(command.Genre);

            _dbContext.Books.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }

    private Guid GetGenreIdFromName(string genreName)
    {
        var genre = _dbContext.Genres.FirstOrDefault(x=> x.Name.ToLower() == genreName.ToLower());

        if(genre != null)
        {
            return genre.Id;
        }
        else
        {
            throw new InvalidOperationException("Genre did not find");
        }
    }
}