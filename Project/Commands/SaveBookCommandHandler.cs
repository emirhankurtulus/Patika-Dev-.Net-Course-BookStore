using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Enums;

namespace Project.Commands;

public class SaveBookCommandHandler
{
    private readonly BookDBContext _dbContext;
    private readonly IMapper _mapper;

    public SaveBookCommandHandler(BookDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(SaveBookCommand entity)
    {
        GenreEnum genreEnumValue;
        int? genreId = null;

        if (Enum.TryParse(entity.Genre, out genreEnumValue))
        {
            genreId = (int)genreEnumValue;
        }
        else
        {
            throw new InvalidOperationException("Genre did not find");
        }

        if (entity.Id is not null)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == entity.Id);

            if (book is null)
            {
                throw new InvalidOperationException("Book did not find");
            }

            book = _mapper.Map<Book>(entity);

            book.PublishDate = entity.PublishDate;
            book.GenreId = GetGenreIdFromName(entity.Genre);
            book.PageCount = entity.PageCount;
            book.Title = entity.Title;

            _dbContext.Books.Update(book);

            _dbContext.SaveChanges();
        }
        else
        {
            var createdEntity = _mapper.Map<Book>(entity);

            _dbContext.Books.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }

    private int GetGenreIdFromName(string genreName)
    {
        GenreEnum genreEnumValue;
        int? genreId = null;

        if (Enum.TryParse(genreName, out genreEnumValue))
        {
            genreId = (int)genreEnumValue;

            return genreId.Value;
        }
        else
        {
            throw new InvalidOperationException("Genre did not find");
        }
    }

}