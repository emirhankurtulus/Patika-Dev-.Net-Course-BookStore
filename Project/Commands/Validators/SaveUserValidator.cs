using FluentValidation;

namespace Project.Commands.Validators;

public class SaveUserValidator : AbstractValidator<SaveUserCommand>
{
    public SaveUserValidator()
    {
        RuleFor(command => command.FirstName).MinimumLength(1).MaximumLength(50).NotEmpty();
        RuleFor(command => command.Surname).MinimumLength(1).MaximumLength(50).NotEmpty();
        RuleFor(command => command.Email).EmailAddress().NotEmpty();
    }
}