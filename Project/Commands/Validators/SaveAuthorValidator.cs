using FluentValidation;
using Project.Commands.Books;

namespace Project.Commands.Validators;

public class SaveAuthorValidator : AbstractValidator<SaveAuthorCommand>
{
    public SaveAuthorValidator()
    {
        RuleFor(command => command.FirstName).MinimumLength(2).MaximumLength(50).NotEmpty();
        RuleFor(command => command.Surname).MinimumLength(1).MaximumLength(50).NotEmpty();
        RuleFor(command => command.BirthDate).LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-3))).NotNull();
    }
}