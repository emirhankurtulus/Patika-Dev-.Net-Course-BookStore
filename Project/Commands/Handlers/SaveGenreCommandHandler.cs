using AutoMapper;
using Project.DBOperations;
using Project.Entities;

namespace Project.Commands.Handlers;

public class SaveGenreCommandHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaveGenreCommandHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(SaveGenreCommand command)
    {
        if (command.Id is not null)
        {
            var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == command.Id);

            if (genre is null)
            {
                throw new InvalidOperationException($"Genre id= {command.Id} did not find");
            }

            if (genre.Name != command.Name)
            {
                var filteredGenre = _dbContext.Genres.Where(x => x.Name.ToLower() == command.Name.ToLower());

                if (filteredGenre.Any())
                {
                    throw new InvalidOperationException($"{command.Name} already exist");
                }
            }

            if (command.Active.Value)
            {
                genre.Active = command.Active.Value;
            }

            genre.Name = command.Name;

            _dbContext.Genres.Update(genre);

            _dbContext.SaveChanges();
        }
        else
        {
            var filteredGenre = _dbContext.Genres.Where(x => x.Name.ToLower() == command.Name.ToLower());

            if (filteredGenre.Any())
            {
                throw new InvalidOperationException($"{command.Name} already exist");
            }

            var createdEntity = _mapper.Map<Genre>(command);

            _dbContext.Genres.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }
}