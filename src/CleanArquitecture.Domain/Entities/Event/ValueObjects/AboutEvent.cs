using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record AboutEvent
{
    public string Value { get; }

    private AboutEvent(string value)
    {
        Value = value;
    }

    public static Result<AboutEvent> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
            return Result.Failure<AboutEvent>(EventErrors.InvalidAboutEvent);

        return Result.Success(new AboutEvent(value));
    }
}
