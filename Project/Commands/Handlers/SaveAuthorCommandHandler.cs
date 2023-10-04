using AutoMapper;
using Project.DBOperations;
using Project.Entities;

namespace Project.Commands.Handlers;

public class SaveAuthorCommandHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaveAuthorCommandHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(SaveAuthorCommand command)
    {
        if (command.Id is not null)
        {
            var author = _dbContext.Authors.FirstOrDefault(x => x.Id == command.Id);

            if (author is null)
            {
                throw new InvalidOperationException($"Author id={command.Id} did not find");
            }

            author.BirthDate = command.BirthDate;
            author.FirstName = command.FirstName;
            author.Surname = command.Surname;

            _dbContext.Authors.Update(author);

            _dbContext.SaveChanges();
        }
        else
        {
            var createdEntity = _mapper.Map<Author>(command);

            _dbContext.Authors.Add(createdEntity);

            _dbContext.SaveChanges();
        }
    }
}