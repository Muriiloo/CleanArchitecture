using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Event;

public sealed class Event : Entity<EventId>
{
    private Event(EventId id, NameEvent nameEvent, DescriptionEvent descriptionEvent) : base(id)
    {
        NameEvent = nameEvent;
        DescriptionEvent = descriptionEvent;
    }
    
    public NameEvent NameEvent { get; private set; }
    public DescriptionEvent DescriptionEvent { get; private set; }
    public string Location { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public DateTime InitialDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int LimitAge { get; private set; }
    public string AboutEvent { get; private set; }
}