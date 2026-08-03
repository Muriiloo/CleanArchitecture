using CleanArquitecture.Application.Authentication;
using CleanArquitecture.Application.Customers.AuthenticateCustomer;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;
using CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;
using CleanArquitecture.Test.Application.Authentication;

namespace CleanArquitecture.Test.Application.Customers;

public class AuthenticateCustomerHandlerTest
{
    private readonly InMemoryCustomerRepository _customerRepo;
    private readonly AuthenticateCustomerHandler _handler;
    private readonly IJwtProvider _jwtProvider;
    private readonly CancellationToken _cancellationToken;

    public AuthenticateCustomerHandlerTest()
    {
        _customerRepo = new InMemoryCustomerRepository();
        _jwtProvider = new FakeJwtProvider();
        _handler = new AuthenticateCustomerHandler(_customerRepo, _jwtProvider);
    }

    [Fact]
    public async Task Handle_OnSuccess_ShouldReturnToken()
    {
        var fullName = FullName.Create("Murilo");
        var password = Password.Create("1234567");
        var email = Email.Create("murilo@gmail.com");
        var cpf = Cpf.Create("53140598009");
        var birthDay = BirthDay.Create(new DateOnly(2005,3,11));

        var customer = Customer.Create(fullName.Value, password.Value, email.Value, cpf.Value, birthDay.Value);
        _customerRepo.Add(customer.Value);

        var command = new AuthenticateCustomerCommand("murilo@gmail.com", "1234567");
        var result = await _handler.Handle(command, _cancellationToken);

        Assert.NotEmpty(result.Value);
        Assert.IsType<string>(result.Value);
    }

    [Theory]
    [InlineData("invalid-email", "")] // os dois invalidos
    [InlineData("murilo@gmail.com", "")] // senha invalida
    [InlineData("invalid-email", "1234567")] // senha invalida
    public async Task Handle_WhenAuthenticationDataIsInvalid_ShouldReturnFailure(string email, string password)
    {
        var command = new AuthenticateCustomerCommand(email, password);
        var result = await _handler.Handle(command, _cancellationToken);

        Assert.NotEmpty(result.Errors);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCustomerNotFoundByEmail_ShouldReturnFailure()
    {
        var command = new AuthenticateCustomerCommand("murilo@gmail.com", "1234567");
        var result = await _handler.Handle(command, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsIncorrect_ShouldReturnFailure()
    {
        var fullName = FullName.Create("Murilo");
        var password = Password.Create("1234567");
        var email = Email.Create("murilo@gmail.com");
        var cpf = Cpf.Create("53140598009");
        var birthDay = BirthDay.Create(new DateOnly(2005, 3, 11));

        var customer = Customer.Create(fullName.Value, password.Value, email.Value, cpf.Value, birthDay.Value);
        _customerRepo.Add(customer.Value);

        var command = new AuthenticateCustomerCommand("murilo@gmail.com", "12345678");
        var result = await _handler.Handle(command, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }

}
