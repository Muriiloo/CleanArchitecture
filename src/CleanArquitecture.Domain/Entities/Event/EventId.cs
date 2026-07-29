namespace CleanArquitecture.Domain.Entities.Event;

public record EventId(Guid Value)
{
    public static EventId FromValue(Guid Value) => new EventId(Value);
    public static EventId New() => FromValue(Guid.NewGuid());
}