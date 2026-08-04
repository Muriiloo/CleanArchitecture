using CleanArquitecture.Application.Event.CreateEvent;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;
using CleanArquitecture.Test.Domain.Time;
using CleanArquitecture.Test.InMemoryRepositories;

namespace CleanArquitecture.Test.Application.Event;

public class CreateEventHandlerTest
{
    private readonly InMemoryEventRepository _eventRepo;
    private readonly InMemoryUnitOfWorkRepository _unitOfWork;
    private readonly IDateTimeProvider _time;
    private readonly CreateEventHandler _handler;
    private CancellationToken _cancellationToken;

    public CreateEventHandlerTest()
    {
        _eventRepo = new InMemoryEventRepository();
        _unitOfWork = new InMemoryUnitOfWorkRepository();
        _time = new FixedDateTimeProvider();
        _handler = new CreateEventHandler(_eventRepo, _unitOfWork, _time);
    }

    [Fact]
    public async Task Handle_Should_Create_Event_And_Save()
    {
        var command = new CreateEventCommand(
            "Show Dupla Sertaneja",
            "A dupla mais escutada do mundo está de volta.",
            "Rua fulano de tal, 123",
            "São Paulo",
            "SP",
            _time.UtcNow.AddDays(7),
            _time.UtcNow.AddDays(8),
            18,
            "Dupla sertaneja que está fazendo sucesso tal");

        var result = await _handler.Handle(command, _cancellationToken);

        var eventId = EventId.FromValue(result.Value);

        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.True(result.IsSuccess);

        var saved = await _eventRepo.GetByIdAsync(eventId, _cancellationToken);

        Assert.NotNull(saved);
        Assert.Equal("Show Dupla Sertaneja", saved.NameEvent.Value);
    }

    [Theory]
    [InlineData("A", "descrição válida com mais de dez caracteres", "Rua fulano de tal, 123", "São Paulo", "SP", 18, "sobre o evento válido com mais de seis")] // somente nome errado
    [InlineData("Show Dupla Sertaneja", "curto", "Rua fulano de tal, 123", "São Paulo", "SP", 18, "sobre o evento válido com mais de seis")] // somente descrição errada
    [InlineData("Show Dupla Sertaneja", "descrição válida com mais de dez caracteres", "rua", "São Paulo", "SP", 18, "sobre o evento válido com mais de seis")] // somente local errado
    [InlineData("Show Dupla Sertaneja", "descrição válida com mais de dez caracteres", "Rua fulano de tal, 123", "sp", "SP", 18, "sobre o evento válido com mais de seis")] // somente cidade errada
    [InlineData("Show Dupla Sertaneja", "descrição válida com mais de dez caracteres", "Rua fulano de tal, 123", "São Paulo", "S", 18, "sobre o evento válido com mais de seis")] // somente estado errado
    [InlineData("Show Dupla Sertaneja", "descrição válida com mais de dez caracteres", "Rua fulano de tal, 123", "São Paulo", "SP", 17, "sobre o evento válido com mais de seis")] // somente idade mínima errada
    [InlineData("Show Dupla Sertaneja", "descrição válida com mais de dez caracteres", "Rua fulano de tal, 123", "São Paulo", "SP", 18, "abcde")] // somente sobre errado
    public async Task Handle_WhenFieldsAreInvalid_ShouldReturnFailure(string name, string description, string location, string city, string state, int limitAge, string about)
    {
        var command = new CreateEventCommand(
            name,
            description,
            location,
            city,
            state,
            _time.UtcNow.AddDays(7),
            _time.UtcNow.AddDays(8),
            limitAge,
            about);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, _cancellationToken));

        Assert.NotEmpty(exception.Errors);
    }

    [Fact]
    public async Task Handle_WhenStartDateIsInThePast_ShouldReturnFailure()
    {
        var command = new CreateEventCommand(
            "Show Dupla Sertaneja",
            "descrição válida com mais de dez caracteres",
            "Rua fulano de tal, 123",
            "São Paulo",
            "SP",
            _time.UtcNow.AddDays(-1),
            _time.UtcNow.AddDays(8),
            18,
            "sobre o evento válido com mais de seis");

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, _cancellationToken));

        Assert.NotEmpty(exception.Errors);
    }

    [Fact]
    public async Task Handle_WhenStartDateIsAfterEndDate_ShouldReturnFailure()
    {
        var command = new CreateEventCommand(
            "Show Dupla Sertaneja",
            "descrição válida com mais de dez caracteres",
            "Rua fulano de tal, 123",
            "São Paulo",
            "SP",
            _time.UtcNow.AddDays(7),
            _time.UtcNow.AddDays(6),
            18,
            "sobre o evento válido com mais de seis");

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, _cancellationToken));

        Assert.NotEmpty(exception.Errors);
    }
}
