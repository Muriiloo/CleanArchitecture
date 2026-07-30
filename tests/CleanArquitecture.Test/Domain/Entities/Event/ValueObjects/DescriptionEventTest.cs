using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class DescriptionEventTest
{
    [Fact]
    public static void Create_Should_ReturnSuccess_When_DescriptionIsValid()
    {
        var description = "Evento vai ser realizado no local tal horario tal";

        var result = DescriptionEvent.Create(description);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public static void Creation_MustReturnAFailure_If_TheDescriptionIs_ShorterThanTenLetters()
    {
        var description = "Evento va";

        var result = DescriptionEvent.Create(description);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidDescriptionEvent, result.Error);
    }

    [Fact]
    public static void Creation_MustReturnAFailure_If_TheDescriptionIs_Empty()
    {
        var description = "";

        var result = DescriptionEvent.Create(description);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidDescriptionEvent, result.Error);
    }
}
