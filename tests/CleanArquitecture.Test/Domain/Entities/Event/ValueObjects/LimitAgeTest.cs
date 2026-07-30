using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class LimitAgeTest
{
    [Fact]
    public static void Create_ShouldReturnSuccess_ForValidAge()
    {
        var age = 18;

        var result = LimitAge.Create(age);

        Assert.True(result.IsSuccess);
        Assert.Equal(age, result.Value.Value);
    }

    [Fact]
    public static void Create_ShouldReturnFailure_ForAgeLessThan18()
    {
        var age = 17;

        var result = LimitAge.Create(age);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidLimitAge, result.Error);
    }

    [Fact]
    public static void Create_ShouldReturnFailure_WhenAgeIsGreaterThan90()
    {
        var age = 91;

        var result = LimitAge.Create(age);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.BadRequestError, result.Error);
    }
}
