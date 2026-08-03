using CleanArquitecture.Application.Authentication;
using CleanArquitecture.Application.Customers.AuthenticateCustomer;
using CleanArquitecture.Application.Producer.AuthenticateProducer;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;
using CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;
using CleanArquitecture.Test.Application.Authentication;

namespace CleanArquitecture.Test.Application.Producers;

public class AuthenticateProducerHandlerTest
{
    private readonly InMemoryProducerRepository _producerRepo;
    private readonly AuthenticateProducerHandler _handler;
    private readonly IJwtProvider _jwtProvider;
    private readonly CancellationToken cancellationToken;

    public AuthenticateProducerHandlerTest()
    {
        _producerRepo = new InMemoryProducerRepository();
        _jwtProvider = new FakeJwtProvider();
        _handler = new AuthenticateProducerHandler(_producerRepo, _jwtProvider);
    }

    [Fact]
    public async Task Handle_OnSuccess_ShouldReturnToken()
    {
        var name = Name.Create("oNofre Casa Noturna");
        var description = Description.Create("Somos uma casa noturna");
        var cnpj = Cnpj.Create("89106713000148");
        var email = Email.Create("onofre@gmail.com");
        var password = Password.Create("valid-password");

        var producer = Producer.Create(name.Value, description.Value, cnpj.Value, email.Value, password.Value);
        _producerRepo.Add(producer.Value);
        var command = new AuthenticateProducerCommand("onofre@gmail.com", "valid-password");

        var result = await _handler.Handle(command, cancellationToken);

        Assert.True(result.IsSuccess);
        Assert.IsType<string>(result.Value);
    }

    [Theory]
    [InlineData("invalid-email", "")] // os dois invalidos
    [InlineData("murilo@gmail.com", "")] // senha invalida
    [InlineData("invalid-email", "1234567")] // senha invalida
    public async Task Handle_WhenAuthenticationDataIsInvalid_ShouldReturnFailure(string email, string password)
    {
        var command = new AuthenticateProducerCommand(email, password);
        var result = await _handler.Handle(command, cancellationToken);

        Assert.NotEmpty(result.Errors);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCustomerNotFoundByEmail_ShouldReturnFailure()
    {
        var command = new AuthenticateProducerCommand("murilo@gmail.com", "1234567");
        var result = await _handler.Handle(command, cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsIncorrect_ShouldReturnFailure()
    {
        var name = Name.Create("oNofre Casa Noturna");
        var description = Description.Create("Somos uma casa noturna");
        var cnpj = Cnpj.Create("89106713000148");
        var email = Email.Create("onofre@gmail.com");
        var password = Password.Create("valid-password");

        var producer = Producer.Create(name.Value, description.Value, cnpj.Value, email.Value, password.Value);
        _producerRepo.Add(producer.Value);
        var command = new AuthenticateProducerCommand("onofre@gmail.com", "invalid-password");

        var result = await _handler.Handle(command, cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.Unauthorized, result.Error);
    }
}
