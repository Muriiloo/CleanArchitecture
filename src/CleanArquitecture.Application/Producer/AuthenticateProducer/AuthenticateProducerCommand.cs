using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Producer.AuthenticateProducer;

public record AuthenticateProducerCommand(string Email, string Password) : ICommand<string>;
