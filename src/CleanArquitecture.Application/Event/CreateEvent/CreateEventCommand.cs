using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Event.CreateEvent;

public record CreateEventCommand(
    string NameEvent, 
    string DescriptionEvent, 
    string Location, 
    string City, 
    string State,
    DateTime StartDate,
    DateTime EndDate,
    int LimitAge,
    string AboutEvent) : ICommand<Guid>;
