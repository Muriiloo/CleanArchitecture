using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Customer;

public static class CustomerErrors
{
    public static Error InvalidName = new("invalid.name", "Name is not valid.");
    public static Error InvalidPassword = new("invalid.password", "Password is not valid.");
    public static Error InvalidEmail = new("invalid.email", "Email is not valid.");
    public static Error InvalidCpf = new("invalid.cpf", "Cpf is not valid.");
    public static Error UnauthorizedAge = new("unauthorized.age", "Unauthorized age.");
    public static Error NotFound = new("not.found", "Not found customer.");
}
