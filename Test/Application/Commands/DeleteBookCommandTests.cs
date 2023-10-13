using AutoMapper;
using FluentAssertions;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIdIsNotExist_InvalidOperationsException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();
        var command = new DeleteBookCommandHandler(_context);


        FluentActions.Invoking(() => command.Handle(id))
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Book id= {id} did not find");
    }

    [Fact]
    public void WhenGivenBookIdIsExist_ShouldBeDelete()
    {
        var existBook =  _context.Books.FirstOrDefault();

        var command = new DeleteBookCommandHandler(_context);

        FluentActions.Invoking(() => command.Handle(existBook.Id)).Invoke();

        var notExistBook = _context.Books.FirstOrDefault(b => b.Id == existBook.Id);
        notExistBook.Should().BeNull();
    }
}