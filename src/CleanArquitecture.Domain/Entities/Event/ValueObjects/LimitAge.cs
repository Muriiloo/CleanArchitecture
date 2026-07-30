using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Shared.Errors;

namespace CleanArquitecture.Domain.Entities.Event.ValueObjects;

public record LimitAge
{
    public int Value { get; }

    private LimitAge(int value)
    {
        Value = value;
    }

    public static Result<LimitAge> Create(int value)
    {
        if (value < 18)
            return Result.Failure<LimitAge>(EventErrors.InvalidLimitAge);

        if (value > 90)
            return Result.Failure<LimitAge>(GlobalErrors.BadRequestError);

        return Result.Success(new LimitAge(value));
    }

    public static LimitAge FromPersistence(int value) => new(value);
}
