using CleanArquitecture.Application.Customers.CreateCustomer;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Shared.Errors;
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

    [Theory]
    [InlineData("A", "6", "invalid-email", "invalid-cpf", 1899, 1, 24)] // todos campos errados
    [InlineData("A", "1234567", "murilo@gmail.com", "78972346063", 2005, 03, 11)] // somente nome errado
    [InlineData("Murilo Silva", "1", "murilo@gmail.com", "78972346063", 2005, 03, 11)] // somente senha errada
    [InlineData("Murilo Silva", "1234567", "invalid-email", "78972346063", 2005, 03, 11)] // somente email errado
    [InlineData("Murilo Silva", "1234567", "murilo@gmail.com", "invalid-cpf", 2005, 03, 11)] // somente cpf errado
    [InlineData("Murilo Silva", "1234567", "murilo@gmail.com", "78972346063", 1899, 1, 24)] // somente data errada
    public async Task Handle_WhenFieldsAreInvalid_ShouldReturnFailure(string fullName, string password, string email, string cpf, int year, int month, int day)
    {
        var command = new CreateCustomerCommand(fullName, password, email, cpf, new DateOnly(year, month, day));

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, _cancellationToken));

        Assert.NotEmpty(exception.Errors);
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldReturnFailure()
    {
        var customerOne = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "07296922052", new DateOnly(2005, 3, 11));

        await _handler.Handle(customerOne, _cancellationToken);

        var customerTwo = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "04384034016", new DateOnly(2005, 3, 11));

        var result = await _handler.Handle(customerTwo, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.EmailAlreadyExists, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCpfAlreadyExists_ShouldReturnFailure()
    {
        var customerOne = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "04384034016", new DateOnly(2005, 3, 11));

        await _handler.Handle(customerOne, _cancellationToken);

        var customerTwo = new CreateCustomerCommand("Murilo", "123456", "fulano@gmail.com", "04384034016", new DateOnly(2005, 3, 11));

        var result = await _handler.Handle(customerTwo, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(CustomerErrors.CpfAlreadyExists, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCpfAlreadyExistsAndEmailAlreadyExists_ShouldReturnFailure()
    {
        var customerOne = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "04384034016", new DateOnly(2005, 3, 11));

        await _handler.Handle(customerOne, _cancellationToken);

        var customerTwo = new CreateCustomerCommand("Murilo", "123456", "murilo@gmail.com", "04384034016", new DateOnly(2005, 3, 11));

        var result = await _handler.Handle(customerTwo, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(GlobalErrors.EmailAlreadyExists, result.Errors);
        Assert.Contains(CustomerErrors.CpfAlreadyExists, result.Errors);
    }
}
