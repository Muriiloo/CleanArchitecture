using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Shared.Errors;

public static class GlobalErrors
{
    public static Error Unauthorized = new("401", "Unauthorized.");
    public static Error EmailAlreadyExists = new("409", "Email already exists.");
    public static Error BadRequestError = new("400", "Invalid data.");
}
