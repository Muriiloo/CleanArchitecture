using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Application.Producer.CreateProducer;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;
using CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

namespace CleanArquitecture.Test.Application.Producers;

public class CreateProducerHandlerTest
{
    private readonly IProducerRepository _producerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateProducerHandler _handler;
    private CancellationToken _cancellationToken;

    public CreateProducerHandlerTest()
    {
        _producerRepo = new InMemoryProducerRepository();
        _unitOfWork = new InMemoryUnitOfWorkRepository();
        _handler = new CreateProducerHandler(_producerRepo, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_Create_Producer_And_Save()
    {
        var command = new CreateProducerCommand("Casa de shows", 
            "1234567",
            "casadeshows@gmail.com", 
            "82794066000165",
            "Somos uma casa de shows inaugurada em tal de tal");

        var result = await _handler.Handle(command, _cancellationToken);
        var producerId = ProducerId.FromValue(result.Value);
        
        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.True(result.IsSuccess);

        var saved = await _producerRepo.GetByIdAsync(producerId, _cancellationToken);

        Assert.NotNull(saved);
        Assert.Equal(producerId, saved.Id);
    }

    [Theory]
    [InlineData("a", "", "invalid-email", "32313131", "isas")]
    [InlineData("a", "1234567", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal")]
    [InlineData("Casa de shows", "", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal")]
    [InlineData("Casa de shows", "1234567", "invalid-email", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal")]
    [InlineData("Casa de shows", "1234567", "casadeshows@gmail.com", "invalid-cnpj", "Somos uma casa de shows inaugurada em tal de tal")]
    [InlineData("Casa de shows", "1234567", "casadeshows@gmail.com", "invalid-cnpj", "sdd")]
    public async Task Handle_WhenFieldsAreInvalid_ShouldReturnFailure(string name, string password, string email, string cnpj, string description)
    {
        var command = new CreateProducerCommand(name, password, email, cnpj, description);
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, _cancellationToken));
        
        Assert.NotEmpty(exception.Errors);
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldReturnFailure()
    {
        var producerOne = new CreateProducerCommand("Casa de shows", "1234567", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal");
        await _handler.Handle(producerOne, _cancellationToken);
        
        var producerTwo = new CreateProducerCommand("Outra casa de shows", "12345678", "casadeshows@gmail.com", "83173016000123", "Somos uma casa de shows inaugurada em tal de tal");
        var result = await _handler.Handle(producerTwo, _cancellationToken);

        Assert.True(result.IsFailure);
        Assert.Equal(GlobalErrors.EmailAlreadyExists, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCnpjAlreadyExists_ShouldReturnFailure()
    {
        var producerOne = new CreateProducerCommand("Casa de shows", "1234567", "outracasadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal");
        await _handler.Handle(producerOne, _cancellationToken);
        
        var producerTwo = new CreateProducerCommand("Casa de shows", "1234567", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal");
        var result = await _handler.Handle(producerTwo, _cancellationToken);
        
        Assert.True(result.IsFailure);
        Assert.Equal(ProducerErrors.CnpjAlreadyExists, result.Error);
    }

    [Fact]
    public async Task Handle_WhenCnpjAlreadyExistsAndEmailAlreadyExists_ShouldReturnFailure()
    {
        var producerOne = new CreateProducerCommand("Casa de shows", "1234567", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal");
        await _handler.Handle(producerOne, _cancellationToken);
        
        var producerTwo = new CreateProducerCommand("Casa de shows", "1234567", "casadeshows@gmail.com", "82794066000165", "Somos uma casa de shows inaugurada em tal de tal");
        var result = await _handler.Handle(producerTwo, _cancellationToken);
        
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(ProducerErrors.CnpjAlreadyExists, result.Errors);
        Assert.Contains(GlobalErrors.EmailAlreadyExists, result.Errors);
    }
}