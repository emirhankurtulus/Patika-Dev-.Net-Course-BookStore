using AutoMapper;
using Project.Commands;
using Project.Commands.Books;
using Project.DBOperations;
using Project.DTO;
using Project.Entities;

namespace Project.Mapping;

public class MappingProfile : Profile
{
    private readonly BookStoreDbContext _dbContext;

    public MappingProfile(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<SaveAuthorCommand, Author>();

        CreateMap<Book, BookDto>();
        CreateMap<SaveBookCommand, Book>();

        CreateMap<Genre, GenreDto>();
        CreateMap<SaveGenreCommand, Genre>();
    }
}