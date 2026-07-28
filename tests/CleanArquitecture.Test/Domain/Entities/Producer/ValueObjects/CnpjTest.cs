using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Producer.ValueObjects;

public class CnpjTest
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_CnpjIsValid()
    {
        var cpf = "34142462000165";
        var result = Cnpj.Create(cpf);

        Assert.True(result.IsSuccess);
        Assert.Equal(cpf, result.Value.Value);
    }

    [Fact]
    public void Create_Should_ReturnFailure_When_CnpjIsNotValid()
    {
        var cpf = "00000000000000000";
        var result = Cnpj.Create(cpf);

        Assert.True(result.IsFailure);
        Assert.Equal(ProducerErrors.InvalidCnpj, result.Error);
    }
}