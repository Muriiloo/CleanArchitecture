using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Producer.ValueObjects;

public record Name
{
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Name>(ProducerErrors.InvalidName);

        if (fullName.Length < 3)
            return Result.Failure<Name>(ProducerErrors.InvalidName);

        return Result.Success(new Name(fullName));
    }

    public static Name FromPersistence(string value) => new(value);
}