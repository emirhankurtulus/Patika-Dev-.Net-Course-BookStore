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
        CreateMap<SaveBookCommand, Book>();
        CreateMap<Book, BookDto>();

        CreateMap<Genre, GenreDto>();
        CreateMap<SaveGenreCommand, Genre>();
    }
}