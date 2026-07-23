using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using Moq;

namespace CleanArquitecture.Test.Application.Customers;

public class CreateCustomerHandlerTest
{
    [Fact]
    public async Task Handle_Should_Create_Customer_And_Save()
    {
        var mockCustomerRepo = new Mock<ICustomerRepository>();
        var mockUnitOfWorkRepo = new Mock<IUnitOfWork>();

        mockUnitOfWorkRepo
        .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(1);
    }
}
