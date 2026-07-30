using CleanArquitecture.Application.Customers.CreateCustomer;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

namespace CleanArquitecture.Test.Application.Customers;

public class CreateCustomerHandlerTest
{
    private readonly InMemoryCustomerRepository _customerRepo;
    private readonly InMemoryUnitOfWorkRepository _unitOfWork;
    private readonly CreateCustomerHandler _handler;
    private CancellationToken _cancellationToken;

    //construtor vai rodar antes de cada teste garantindo o isolamento
    public CreateCustomerHandlerTest()
    {
        _customerRepo = new InMemoryCustomerRepository();
        _unitOfWork = new InMemoryUnitOfWorkRepository();
        _handler = new CreateCustomerHandler(_customerRepo, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_Create_Customer_And_Save()
    {
        var command = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "07296922052", new DateOnly(2005,3,11));

        var result = await _handler.Handle(command, _cancellationToken);

        var customerId = CustomerId.FromValue(result.Value);

        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.True(result.IsSuccess);

        var saved = await _customerRepo.GetByIdAsync(customerId, _cancellationToken);

        Assert.NotNull(saved);
        Assert.Equal("murilo@gmail.com", saved.Email.Value);
    }
}
