using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Infraestructure.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
