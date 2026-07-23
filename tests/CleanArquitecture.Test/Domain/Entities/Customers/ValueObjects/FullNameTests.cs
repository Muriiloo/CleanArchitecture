using CleanArquitecture.Domain.Entities.Customer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Customers.ValueObjects;

public class FullNameTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_NameIsValid()
    {
        var name = "Murilo de Oliveira";

        var result = FullName.Create(name);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Creation_Should_ReturnAnError_When_Empty()
    {
        var name = "";

        var result = FullName.Create(name);

        Assert.True(result.isFailure);
    }

    [Fact]
    public void Creation_Must_ReturnAnError_When_The_Character_LengthIsLess_Than_Three()
    {
        var name = "An";

        var result = FullName.Create(name);

        Assert.True(result.isFailure);
    }
}
