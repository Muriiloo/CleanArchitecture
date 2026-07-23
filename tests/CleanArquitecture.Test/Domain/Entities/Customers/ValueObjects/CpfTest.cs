using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Customers.ValueObjects;

public class CpfTest
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_CpfIsValid()
    {
        var cpf = "44279926840";
        var result = Cpf.Create(cpf);

        Assert.True(result.IsSuccess);
        Assert.Equal(cpf, result.Value.Value);
    }

    [Fact]
    public void Create_Should_ReturnFailure_When_CpfIsNotValid()
    {
        var cpf = "00000000000000000";
        var result = Cpf.Create(cpf);

        Assert.True(result.isFailure);
        Assert.Equal(CustomerErrors.InvalidCpf, result.Error);
    }
}
