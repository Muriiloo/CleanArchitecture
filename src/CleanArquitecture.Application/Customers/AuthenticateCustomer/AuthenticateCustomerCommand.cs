using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Customers.AuthenticateCustomer;

public record AuthenticateCustomerCommand(string Email, string Password) : ICommand<string>;
