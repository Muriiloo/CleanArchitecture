using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record DescriptionEvent
{
    public string Value { get; private set; }

    private DescriptionEvent(string value)
    {
        Value = value;
    }

    public static Result<DescriptionEvent> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<DescriptionEvent>(EventErrors.InvalidDescriptionEvent);

        if (value.Length < 10)
            return Result.Failure<DescriptionEvent>(EventErrors.InvalidDescriptionEvent);
        
        return Result.Success(new DescriptionEvent(value));
    }
}