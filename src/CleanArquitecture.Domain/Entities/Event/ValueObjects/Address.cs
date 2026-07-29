using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record Address
{
    private Address(string location, string city, string state)
    {
        Location = location;
        City = city;
        State = state;
    }
    public string Location { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
}