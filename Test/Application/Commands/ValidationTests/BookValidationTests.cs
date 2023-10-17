using FluentAssertions;
using Project.Commands.Books;
using Project.Commands.Validators;
using Test.TestSetup;

namespace Test.Application.Commands.ValidationTests;

public class BookValidationTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Lord Of The Rings", 0, "genre1")]
    [InlineData("Lord Of The Rings", 0, "classics")]
    [InlineData("Lord Of The Rings", 100, "adventure")]
    [InlineData("", 0, "funny")]
    public void WhenInvalidInputsAreaGiven_Validator_ShouldBeReturnErrors(String title, int pageCount, string genre)
    {
        var book = new SaveBookCommand
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = new DateOnly(2022, 05, 03),
            Genre = genre,
        };

        var validator = new SaveBookValidator();

        var result = validator.Validate(book);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
}