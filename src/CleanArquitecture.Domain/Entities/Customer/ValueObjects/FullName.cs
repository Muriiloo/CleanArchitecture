using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Customer.ValueObjects;

public record FullName
{
    public string Value { get; }

    private FullName(string value)
    {
        Value = value;
    }

    public static Result<FullName> Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<FullName>(CustomerErrors.InvalidName);

        if (fullName.Length < 3)
            return Result.Failure<FullName>(CustomerErrors.InvalidName);

        return Result.Success(new FullName(fullName));
    }
}
