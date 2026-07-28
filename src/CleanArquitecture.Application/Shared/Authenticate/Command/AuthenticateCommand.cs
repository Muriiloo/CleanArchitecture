using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Shared.Authenticate.Command;

public record AuthenticateCommand(string Email, string Password) : ICommand<string>;
