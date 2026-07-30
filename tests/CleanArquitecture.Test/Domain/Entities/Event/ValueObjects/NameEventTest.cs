using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class NameEventTest
{
    [Fact]
    public static void Create_Should_ReturnSuccess_When_NameIsValid()
    {
        var name = "Ce ta doido A Festa";

        var result = NameEvent.Create(name);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public static void Creation_Should_ReturnAnError_When_Empty()
    {
        var name = "";

        var result = NameEvent.Create(name);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidNameEvent, result.Error);
    }

    [Fact]
    public static void Creation_MustReturnAn_ErrorIfThe_NameIsShorter_ThanSix_Letters()
    {
        var name = "Seisl";

        var result = NameEvent.Create(name);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidNameEvent, result.Error);
    }
}
