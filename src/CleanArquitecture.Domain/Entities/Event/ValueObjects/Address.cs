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

    public static Result<Address> Create(string location, string city, string state)
    {
        if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(state))
            return Result.Failure<Address>(EventErrors.InvalidAddressEvent);

        if (city.Length < 3 || location.Length < 4 || state.Length < 2)
            return Result.Failure<Address>(EventErrors.InvalidAddressEvent);

        return Result.Success(new Address(location, city, state));
    }
}