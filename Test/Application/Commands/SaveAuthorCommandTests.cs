using AutoMapper;
using FluentAssertions;
using Project.Commands;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class SaveAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public SaveAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreaGiven_Author_ShouldBeCreated()
    {
        var author = new SaveAuthorCommand
        {
            FirstName = "İbrahim Yiğit",
            Surname = "Erinç",
            BirthDate = new DateOnly(2000, 01, 01),
            Active = true
        };

        var command = new SaveAuthorCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(author)).Invoke();

        var previousAuthor = _context.Authors.FirstOrDefault(a =>
            a.FirstName == author.FirstName &&
            a.Surname == author.Surname &&
            a.BirthDate == author.BirthDate);

        previousAuthor.Should().NotBeNull();
    }

    [Fact]
    public void WhenValidInputsAreaGiven_Author_ShouldBeUpdated()
    {
        var author = new SaveAuthorCommand
        {
            Id = Guid.Parse("717a2028-601d-4485-ab48-1ee95f124ae3"),
            FirstName = "İbrahim Yiğit",
            Surname = "Erinç",
            BirthDate = new DateOnly(2000, 01, 01),
            Active = true
        };

        var command = new SaveAuthorCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(author)).Invoke();

        var previousAuthor = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
        previousAuthor.Should().NotBeNull();
        previousAuthor.FirstName.Should().Be(author.FirstName);
        previousAuthor.Surname.Should().Be(author.Surname);
        previousAuthor.BirthDate.Should().Be(author.BirthDate);
        previousAuthor.Active.Should().Be(author.Active);
    }

    [Fact]
    public async void WhenGivenAutorToUpdateIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();

        var author = new SaveAuthorCommand
        {
            Id = id,
            FirstName = "İbrahim Yiğit",
            Surname = "Erinç",
            BirthDate = new DateOnly(2000, 01, 01)
        };

        var command = new SaveAuthorCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(author))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Author id={id} did not find");
    }
}