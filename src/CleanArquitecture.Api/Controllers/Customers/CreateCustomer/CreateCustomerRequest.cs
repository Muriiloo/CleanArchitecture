namespace CleanArquitecture.Api.Controllers.Customers.CreateCustomer;

public record CreateCustomerRequest(string FullName, string Password, string Email, string Cpf, DateOnly BirthDay);
