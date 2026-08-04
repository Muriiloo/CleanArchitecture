using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;
using CleanArquitecture.Test.Domain.Time;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class PeriodEventTest
{
    private readonly IDateTimeProvider _time;

    public PeriodEventTest()
    {
        _time = new FixedDateTimeProvider();
    }

    [Fact]
    public void Create_WhenPeriodIsValid_ShouldReturnSuccess()
    {
        var startDate = _time.UtcNow.AddDays(7);
        var endDate = startDate.AddHours(6);

        var result = PeriodEvent.Create(startDate, endDate, _time);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_WhenStartDateIsInThePast_ShouldReturnFailure()
    {
        var startDate = _time.UtcNow.AddDays(-1);
        var endDate = _time.UtcNow.AddDays(2);

        var result = PeriodEvent.Create(startDate, endDate, _time);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidPeriodEvent, result.Error);
    }

    [Fact]
    public void Create_WhenStartDateIsAfterEndDate_ShouldReturnFailure()
    {
        var startDate = _time.UtcNow.AddDays(7);
        var endDate = startDate.AddHours(-6);

        var result = PeriodEvent.Create(startDate, endDate, _time);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidPeriodEvent, result.Error);
    }
}
