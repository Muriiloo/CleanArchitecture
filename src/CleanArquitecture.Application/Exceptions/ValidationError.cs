using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Application.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);

internal static class ValidationErrorMapper
{
    internal static List<ValidationError> GetValidationErrors(params Result[] results)
    {
        return results
            .Where(result => result.IsFailure)
            .Select(failure => new ValidationError(
                failure.Error.Code,
                failure.Error.Name))
            .ToList();
    }
}