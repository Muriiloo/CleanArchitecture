using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Event;

public sealed class Event : Entity<EventId>
{
    private Event(EventId id, NameEvent nameEvent, DescriptionEvent descriptionEvent, Address address, PeriodEvent periodEvent) : base(id)
    {
        NameEvent = nameEvent;
        DescriptionEvent = descriptionEvent;
        Address = address;
        PeriodEvent = periodEvent;

    }
    
    public NameEvent NameEvent { get; private set; }
    public DescriptionEvent DescriptionEvent { get; private set; }
    public Address Address { get; private set; }
    public PeriodEvent PeriodEvent { get; private set; }
    public int LimitAge { get; private set; }
    public string AboutEvent { get; private set; }
}