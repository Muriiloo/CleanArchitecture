using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Customer.ValueObjects;

public record class BirthDay
{
    public DateOnly Value { get; }

    private BirthDay(DateOnly birthDay)
    {
        Value = birthDay;
    }

    public static Result<BirthDay> Create(DateOnly date)
    {
        var age = DateTime.Today.Year - date.Year;

        if (age < 18)
            return Result.Failure<BirthDay>(CustomerErrors.UnauthorizedAge);

        return Result.Success(new BirthDay(date));
    }
}
