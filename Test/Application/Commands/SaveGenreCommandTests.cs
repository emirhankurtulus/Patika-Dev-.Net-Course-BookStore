using AutoMapper;
using FluentAssertions;
using Project.Commands;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class SaveGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public SaveGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreaGiven_Genre_ShouldBeCreated()
    {
        var genre = new SaveGenreCommand
        {
            Name = "Comic",
            Active = true
        };

        var command = new SaveGenreCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(genre)).Invoke();

        var previousGenre = _context.Genres.SingleOrDefault(b => b.Name == genre.Name);
        previousGenre.Should().NotBeNull();
        previousGenre.Name.Should().Be(genre.Name);
    }

    [Fact]
    public void WhenValidInputsAreaGiven_Genre_ShouldBeUpdated()
    {
        var genre = new SaveGenreCommand
        {
            Id = Guid.Parse("48278b65-4abe-45ab-9ba1-61e296bec4bf"),
            Name = "Comic",
            Active = true
        };

        var command = new SaveGenreCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(genre)).Invoke();

        var previousGenre = _context.Genres.SingleOrDefault(g => g.Id == genre.Id);
        previousGenre.Should().NotBeNull();
        previousGenre.Name.Should().Be(genre.Name);
    }

    [Fact]
    public async void WhenGivenGenreToUpdateIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();

        var genre = new SaveGenreCommand
        {
            Id = id,
            Name = "Comic",
            Active = true
        };

        var command = new SaveGenreCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(genre))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Genre id= {id} did not find");
    }

    [Fact]
    public async void WhenGivenGenreNameIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new SaveGenreCommand
        {
            Name = "Classics",
            Active = true
        };

        var command = new SaveGenreCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(genre))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"{genre.Name} already exist");
    }
}