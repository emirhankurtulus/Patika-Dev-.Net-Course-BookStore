using AutoMapper;
using FluentAssertions;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIdIsNotExist_InvalidOperationsException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();
        var command = new DeleteGenreCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(id))
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Genre id= {id} did not find");
    }

    [Fact]
    public void WhenGivenGenreIdIsExist_ShouldBeDelete()
    {
        var id = Guid.Parse("34de5cd6-8e15-4ce3-970c-2aa2d7e4b46b"); //this genre is not using in any book.

        var command = new DeleteGenreCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(id)).Invoke();

        var notExistGenre = _context.Genres.FirstOrDefault(b => b.Id == id);
        notExistGenre.Should().BeNull();
    }

    [Fact]
    public void WhenGivenGenreIdIsUsedInAnyBooks_ShouldBeDelete()
    {
        var book = _context.Books.FirstOrDefault();

        var command = new DeleteGenreCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(book.GenreId))
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"This Genre id={book.GenreId} is used in any book(s)");
    }
}