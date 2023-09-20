using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Project.Commands;

public class SaveBookValidator : AbstractValidator<SaveBookCommand>
{
    public SaveBookValidator()
    {
        RuleFor(command => command.Genre).NotEmpty();
        RuleFor(command => command.Title).MinimumLength(1).MaximumLength(100).NotEmpty();
        RuleFor(command => command.PageCount).GreaterThan(0).NotEmpty();
        RuleFor(command => command.PublishDate).LessThan(DateOnly.FromDateTime(DateTime.Now)).NotEmpty();
    }
}
