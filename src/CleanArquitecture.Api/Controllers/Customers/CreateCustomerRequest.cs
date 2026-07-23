namespace CleanArquitecture.Api.Controllers.Customers;

public record CreateCustomerRequest(string FullName, string Email, string Cpf, DateOnly BirthDay);
