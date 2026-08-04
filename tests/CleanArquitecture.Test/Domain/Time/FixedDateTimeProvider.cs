using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Test.Domain.Time;

public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => new DateTime(2026, 3, 11, 19, 0, 0);
}
