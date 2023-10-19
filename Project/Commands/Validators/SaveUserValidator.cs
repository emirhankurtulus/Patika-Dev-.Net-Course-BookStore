using FluentValidation;

namespace Project.Commands.Validators;

public class SaveUserValidator : AbstractValidator<SaveUserCommand>
{
    public SaveUserValidator()
    {
        RuleFor(command => command.Password).MinimumLength(8).MaximumLength(20).NotEmpty();
        RuleFor(command => command.FirstName).MinimumLength(1).MaximumLength(50).NotEmpty();
        RuleFor(command => command.Surname).MinimumLength(1).MaximumLength(50).NotEmpty();
        RuleFor(command => command.Email).EmailAddress().MaximumLength(50).NotEmpty();
    }
}