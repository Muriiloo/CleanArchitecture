using CleanArquitecture.Application.Abstrations.Messaging;

namespace CleanArquitecture.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(string FullName, string Password, string Email, string Cpf, DateOnly BirthDay) : ICommand<Guid>;
