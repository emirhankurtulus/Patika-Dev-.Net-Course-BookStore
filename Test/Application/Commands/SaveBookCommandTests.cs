using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Commands.Books;
using Project.Commands.Handlers;
using Project.DBOperations;
using Test.TestSetup;

namespace Test.Application.Commands;

public class SaveBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public SaveBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenTheGivenAuthorIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var book = new SaveBookCommand
        {
            Title = "Title",
            PageCount = 453,
            AuthorId = Guid.NewGuid(),
            Genre = "Classics",
            PublishDate = new DateOnly(2023, 03, 13)
        };

        var command = new SaveBookCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(book))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Author id= {book.AuthorId} did not find");
    }

    [Fact]
    public void WhenTheGivenGenreIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var book = new SaveBookCommand
        {
            Title = "Title",
            PageCount = 453,
            AuthorId = Guid.Parse("717a2028-601d-4485-ab48-1ee95f124ae3"),
            Genre = "Comic",
            PublishDate = new DateOnly(2023, 03, 13)
        };

        var command = new SaveBookCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(book))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"{book.Genre} Genre did not find");
    }

    [Fact]
    public async void WhenValidInputsAreaGiven_Book_ShouldBeUpdated()
    {
        var book = new SaveBookCommand
        {
            Title = "How To Read A Book?",
            AuthorId = Guid.Parse("717a2028-601d-4485-ab48-1ee95f124ae3"),
            PageCount = 1232,
            Genre = "Classics",
            PublishDate = new DateOnly(2021, 03, 13),
        };

        var command = new SaveBookCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(book)).Invoke();

        var genre = await _context.Genres.SingleOrDefaultAsync(b => b.Name.ToLower() == book.Genre.ToLower());

        var previousBook = _context.Books.SingleOrDefault(b => b.Title.ToLower() == book.Title.ToLower());
        previousBook.Should().NotBeNull();
        previousBook.PageCount.Should().Be(book.PageCount);
        previousBook.PublishDate.Should().Be(book.PublishDate);
        previousBook.AuthorId.Should().Be(book.AuthorId);
        genre.Name.ToLower().Should().Be(book.Genre.ToLower());
    }

    [Fact]
    public async void WhenValidInputsAreaGiven_Book_ShouldBeCreated()
    {
        var book = new SaveBookCommand
        {
            Title = "Title",
            PageCount = 453,
            AuthorId = Guid.Parse("717a2028-601d-4485-ab48-1ee95f124ae3"),
            Genre = "Classics",
            PublishDate = new DateOnly(2023, 03, 13)
        };

        var command = new SaveBookCommandHandler(_context, _mapper);

        FluentActions.Invoking(() => command.Handle(book)).Invoke();

        var genre = await _context.Genres.SingleOrDefaultAsync(b => b.Name.ToLower() == book.Genre.ToLower());

        var previousBook = _context.Books.SingleOrDefault(b => b.Title == book.Title);
        previousBook.Should().NotBeNull();
        previousBook.PageCount.Should().Be(book.PageCount);
        previousBook.PublishDate.Should().Be(book.PublishDate);
        previousBook.AuthorId.Should().Be(book.AuthorId);
        genre.Name.ToLower().Should().Be(book.Genre.ToLower());
    }

    [Fact]
    public async void WhenGivenBookToUpdateIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var id = Guid.NewGuid();

        var book = new SaveBookCommand
        {
            Id = id,
            Title = "Title",
            PageCount = 453,
            AuthorId = Guid.Parse("717a2028-601d-4485-ab48-1ee95f124ae3"),
            Genre = "Classics",
            PublishDate = new DateOnly(2023, 03, 13)
        };

        var command = new SaveBookCommandHandler(_context, _mapper);

        FluentActions
            .Invoking(() => command.Handle(book))
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Book id={id} did not find");
    }
}