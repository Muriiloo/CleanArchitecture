using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Event.ValueObjects;

public class AddressTest
{
    [Fact]
    public static void Create_WithValidAddress_ShouldReturnSuccess()
    {
        var location = "Rua fulano de tal 255";
        var city = "Cidade Tal";
        var state = "SP";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public static void Create_WhenAllFieldsAreEmpty_ShouldReturnFailure()
    {
        var location = "";
        var city = "";
        var state = "";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenLocationIsEmpty_ShouldReturnFailure()
    {
        var location = "";
        var city = "Cidade Tal";
        var state = "SP";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenCityIsEmpty_ShouldReturnFailure()
    {
        var location = "Rua tal de tal";
        var city = "";
        var state = "SP";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenStateIsEmpty_ShouldReturnFailure()
    {
        var location = "Rua tal de tal";
        var city = "Cidade de tal";
        var state = "";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenFieldsAreBelowMinLength_ShouldReturnFailure()
    {
        var location = "Rua";
        var city = "Ci";
        var state = "S";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenCityIsBelowMinLength_ShouldReturnFailure()
    {
        var location = "Rua Tal de tal";
        var city = "Ci";
        var state = "SP";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenStateIsBelowMinLength_ShouldReturnFailure()
    {
        var location = "Rua Tal de tal";
        var city = "Cidade Tal";
        var state = "S";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

    [Fact]
    public static void Create_WhenLocationIsBelowMinLength_ShouldReturnFailure()
    {
        var location = "Rua";
        var city = "Cidade Tal";
        var state = "SP";

        var result = Address.Create(location, city, state);

        Assert.True(result.IsFailure);
        Assert.Equal(EventErrors.InvalidAddressEvent, result.Error);
    }

}
