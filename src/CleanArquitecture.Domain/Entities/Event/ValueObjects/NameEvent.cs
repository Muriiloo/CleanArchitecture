using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record NameEvent
{
    public string Value { get; private set; }

    private NameEvent(string value)
    {
        Value = value;
    }

    public static Result<NameEvent> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<NameEvent>(EventErrors.InvalidNameEvent);

        if (value.Length < 6)
            return Result.Failure<NameEvent>(EventErrors.InvalidNameEvent);
        
        return Result.Success<NameEvent>(new NameEvent(value));
    }

    public static NameEvent FromPersistence(string name) => new NameEvent(name);
}