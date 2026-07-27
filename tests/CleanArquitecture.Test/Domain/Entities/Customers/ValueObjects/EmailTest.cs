using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Customers.ValueObjects;

public class EmailTest
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_EmailIsValid()
    {
        var email = "murilo@gmail.com";
        var result = Email.Create(email);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Creation_Should_ReturnAnError_When_Empty()
    {
        var email = "";

        var result = Email.Create(email);

        Assert.True(result.IsFailure);
        Assert.Equal(CustomerErrors.InvalidEmail, result.Error);
    }

    [Fact]
    public void Creation_Should_ReturnAnError_When_EmailIsNotValid()
    {
        var email = "not-valid-email";

        var result = Email.Create(email);

        Assert.True(result.IsFailure);
        Assert.Equal(CustomerErrors.InvalidEmail, result.Error);
    }
}
