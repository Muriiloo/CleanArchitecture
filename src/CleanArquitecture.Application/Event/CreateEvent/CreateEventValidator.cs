using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;
using FluentValidation;

namespace CleanArquitecture.Application.Event.CreateEvent;

public class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    private readonly IDateTimeProvider _time;

    public CreateEventValidator(IDateTimeProvider time)
    {
        _time = time;

        RuleFor(e => e.NameEvent)
            .Must(name => NameEvent.Create(name).IsSuccess)
            .WithMessage("The name is invalid");

        RuleFor(e => e.DescriptionEvent)
            .Must(description => DescriptionEvent.Create(description).IsSuccess)
            .WithMessage("The description is invalid");

        RuleFor(e => e)
            .Must(command => Address.Create(command.Location, command.City, command.State).IsSuccess)
            .WithMessage("Invalid location.");

        RuleFor(e => e)
            .Must(command => PeriodEvent.Create(command.StartDate, command.EndDate, _time).IsSuccess)
            .WithMessage("Invalid date.");

        RuleFor(e => e.LimitAge)
            .Must(limitAge => LimitAge.Create(limitAge).IsSuccess)
            .WithMessage("Under the minimum age.");

        RuleFor(e => e.AboutEvent)
            .Must(about => AboutEvent.Create(about).IsSuccess)
            .WithMessage("Invalid about event.");
    }
}
