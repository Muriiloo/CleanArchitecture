using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;

namespace CleanArquitecture.Domain.Entities.Shared.ValueObjects;

public record Password
{
    public string Value { get; private set; }

    private Password(string password)
    {
        Value = password;
    }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure<Password>(CustomerErrors.InvalidPassword);

        return Result.Success(new Password(password));
    }

    public static Password FromPersistence(string password) => new(password);
}
