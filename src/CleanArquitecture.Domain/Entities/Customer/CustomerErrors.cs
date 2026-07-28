using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Customer;

public static class CustomerErrors
{
    public static Error InvalidName = new("400", "Name is not valid.");
    public static Error InvalidPassword = new("400", "Password is not valid.");
    public static Error InvalidEmail = new("400", "Email is not valid.");
    public static Error InvalidCpf = new("400", "Cpf is not valid.");
    public static Error UnauthorizedAge = new("401", "Unauthorized age.");
    public static Error NotFound = new("404", "Not found customer.");
    public static Error CpfAlreadyExists = new("409", "Cpf already exists.");
}
