using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class AboutEventTest
{
    [Fact]
    public static void Create_WithValidAboutEvent_ShouldReturnSuccess()
    {
        var value = "Esse evento tem grande influencia de tal de tal";
        var aboutEvent = AboutEvent.Create(value);

        Assert.True(aboutEvent.IsSuccess);
        Assert.NotNull(aboutEvent.Value);
    }

    [Fact]
    public static void Create_WithInvalidAboutEvent_ShouldReturnFailure()
    {
        var value = "";
        var aboutEvent = AboutEvent.Create(value);

        Assert.True(aboutEvent.IsFailure);
        Assert.NotNull(aboutEvent.Error);
        Assert.Equal(EventErrors.InvalidAboutEvent, aboutEvent.Error);
    }

    [Fact]
    public static void Create_WhenAboutEventIsShorterThanSixCharacters_ShouldReturnFailure()
    {
        var value = "Oeve";
        var aboutEvent = AboutEvent.Create(value);

        Assert.True(aboutEvent.IsFailure);
        Assert.NotNull(aboutEvent.Error);
        Assert.Equal(EventErrors.InvalidAboutEvent, aboutEvent.Error);
    }
}
