using FluentValidation;

namespace Project.Commands.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(command => command.NewPassword).MinimumLength(8).MaximumLength(20).NotEmpty();
    }
}