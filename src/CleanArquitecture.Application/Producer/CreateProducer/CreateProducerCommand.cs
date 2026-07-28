using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Producer.CreateProducer;

public record CreateProducerCommand(string Name, string Password, string Email, string Cnpj, string Description) : ICommand<Guid>;