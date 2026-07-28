using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Producer.ValueObjects;

public class NameTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_NameIsValid()
    {
        var name = "Murilo de Oliveira";

        var result = Name.Create(name);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Creation_Should_ReturnAnError_When_Empty()
    {
        var name = "";

        var result = Name.Create(name);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Creation_Must_ReturnAnError_When_The_Character_LengthIsLess_Than_Three()
    {
        var name = "An";

        var result = Name.Create(name);

        Assert.True(result.IsFailure);
    }
}