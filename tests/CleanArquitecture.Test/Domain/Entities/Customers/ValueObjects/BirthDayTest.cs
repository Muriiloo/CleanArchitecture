using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Customers.ValueObjects;

public class BirthDayTest
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_BirthDayIsValid()
    {
        var date = new DateOnly(2005,3,11);
        var result = BirthDay.Create(date);

        Assert.True(result.IsSuccess);
        Assert.Equal(date, result.Value.Value);
    }

    [Fact]
    public void Create_Should_ReturnFailure_When_BirthDayIsNotValid()
    {
        var date = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        var result = BirthDay.Create(date);

        Assert.True(result.IsFailure);
        Assert.Equal(CustomerErrors.UnauthorizedAge, result.Error);
    }
}
