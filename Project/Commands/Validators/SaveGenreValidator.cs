using FluentValidation;

namespace Project.Commands.Validators;

public class SaveGenreValidator : AbstractValidator<SaveGenreCommand>
{
    public SaveGenreValidator()
    {
        RuleFor(command => command.Name).MinimumLength(1).MaximumLength(50).NotEmpty();
    }
}