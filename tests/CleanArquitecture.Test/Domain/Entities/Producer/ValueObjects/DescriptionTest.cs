using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;

namespace CleanArquitecture.Test.Domain.Entities.Producer.ValueObjects;

public class DescriptionTest
{
    [Fact]
    public void Create_Should_ReturnSuccess_When_DescriptionIsValid()
    {
        var description = "Murilo Desenvolvedor de Software LTDA";

        var result = Description.Create(description);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
    
    [Fact]
    public void Create_Should_ReturnFailure_When_DescriptionIsNotValid()
    {
        var description = "";

        var result = Description.Create(description);

        Assert.True(result.IsFailure);
        Assert.Equal(ProducerErrors.InvalidDescription, result.Error);
    }
}