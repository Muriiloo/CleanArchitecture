using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Event.Events;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Event;

public sealed class Event : Entity<EventId>
{
    private Event(EventId id, NameEvent nameEvent, DescriptionEvent descriptionEvent, Address address, PeriodEvent periodEvent, LimitAge limitAge, AboutEvent aboutEvent) : base(id)
    {
        NameEvent = nameEvent;
        DescriptionEvent = descriptionEvent;
        Address = address;
        PeriodEvent = periodEvent;
        LimitAge = limitAge;
        AboutEvent = aboutEvent;
    }
    public NameEvent NameEvent { get; private set; }
    public DescriptionEvent DescriptionEvent { get; private set; }
    public Address Address { get; private set; }
    public PeriodEvent PeriodEvent { get; private set; }
    public LimitAge LimitAge { get; private set; }
    public AboutEvent AboutEvent { get; private set; }

    public static Result<Event> Create(NameEvent nameEvent, DescriptionEvent descriptionEvent, Address address, PeriodEvent periodEvent, LimitAge limitAge, AboutEvent aboutEvent)
    {
        var @event = new Event(EventId.New(), nameEvent, descriptionEvent, address, periodEvent, limitAge, aboutEvent);
        @event.RaiseDomainEvent(new EventCreatedDomainEvent(@event.Id));

        return Result.Success(@event);
    }
}