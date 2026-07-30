using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class PeriodEventTest
{
    [Fact]
    public static void Create_WhenPeriodIsValid_ShouldReturnSuccess()
    {
        var startDate = new DateTime(2027,03,11,19,0,0);
        var endDate = new DateTime(2027, 03, 12, 3, 0, 0);

        var result = PeriodEvent.Create(startDate, endDate);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public static void Create_WhenStartDateIsAfterEndDate_ShouldReturnFailure()
    {
        var startDate = new DateTime(2027, 03, 13, 19, 0, 0);
        var endDate = new DateTime(2027, 03, 12, 3, 0, 0);

        var result = PeriodEvent.Create(startDate, endDate);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidPeriodEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenEndDateIsBeforeStartDate_ShouldReturnFailure()
    {
        var startDate = new DateTime(2027, 03, 11, 19, 0, 0);
        var endDate = new DateTime(2027, 03, 10, 3, 0, 0);

        var result = PeriodEvent.Create(startDate, endDate);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidPeriodEvent, result.Error);
    }
}
