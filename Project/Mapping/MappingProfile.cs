using AutoMapper;
using Project.Commands;
using Project.Commands.Books;
using Project.DTO;
using Project.Entities;

namespace Project.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<SaveAuthorCommand, Author>();

        CreateMap<Book, BookDto>();
        CreateMap<SaveBookCommand, Book>();

        CreateMap<Genre, GenreDto>();
        CreateMap<SaveGenreCommand, Genre>();

        CreateMap<User, UserDto>();
        CreateMap<SaveUserCommand, User>();
    }
}