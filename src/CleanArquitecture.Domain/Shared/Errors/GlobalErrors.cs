using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Shared.Errors;

public static class GlobalErrors
{
    public static Error Unauthorized = new("unauthorized", "Unauthorized.");
}
