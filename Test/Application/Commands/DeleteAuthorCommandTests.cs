using FluentAssertions;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIdIsNotExist_InvalidOperationsException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();
        var command = new DeleteAuthorCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(id))
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Author id= {id} did not find");
    }

    [Fact]
    public void WhenGivenAuthorIdIsExist_ShouldBeDelete()
    {
        var id = Guid.Parse("ad12094e-2af6-45e6-922a-14356b7ae657"); //this author is not using in any book.

        var command = new DeleteAuthorCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(id)).Invoke();

        var notExistGenre = _context.Authors.FirstOrDefault(b => b.Id == id);
        notExistGenre.Should().BeNull();
    }

    [Fact]
    public void WhenGivenAuthorIdIsUsedInAnyBooks_ShouldBeDelete()
    {
        var book = _context.Books.FirstOrDefault();

        var command = new DeleteAuthorCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(book.AuthorId))
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"This Author id={book.AuthorId} is used in any book(s)");
    }
}