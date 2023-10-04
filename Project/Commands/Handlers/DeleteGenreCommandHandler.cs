﻿using Project.DBOperations;

namespace Project.Commands.Handlers;

public class DeleteGenreCommandHandler
{
    private readonly BookStoreDbContext _dbContext;


    public DeleteGenreCommandHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(Guid id)
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);

        if (genre is null)
        {
            throw new InvalidOperationException("Book did not find");
        }
        else
        {
            var books = _dbContext.Books.Where(x => x.GenreId == id).ToArray();

            if (books is null || books.Length > 0)
            {
                throw new InvalidOperationException("This Genre is used in any book(s)");
            }

            _dbContext.Genres.Remove(genre);

            _dbContext.SaveChanges();
        }
    }
}