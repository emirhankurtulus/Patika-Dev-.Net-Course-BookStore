using AutoMapper;
using Project.Commands;
using Project.DTO;
using Project.Enums;

namespace Project.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SaveBookCommand, Book>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => GetGenreIdFromName(src.Genre)));

        CreateMap<Book, BookDto>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
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