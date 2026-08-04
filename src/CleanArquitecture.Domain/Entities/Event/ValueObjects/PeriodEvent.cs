using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record PeriodEvent
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private PeriodEvent(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<PeriodEvent> Create(DateTime startDate, DateTime endDate, IDateTimeProvider time)
    {
        if (startDate < time.UtcNow)
            return Result.Failure<PeriodEvent>(EventErrors.InvalidPeriodEvent);

        if (startDate > endDate)
            return Result.Failure<PeriodEvent>(EventErrors.InvalidPeriodEvent);

        return Result.Success(new PeriodEvent(startDate, endDate));
    }
}
