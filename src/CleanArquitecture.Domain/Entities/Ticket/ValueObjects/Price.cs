using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Ticket.ValueObjects;

public record Price
{
    public decimal Value { get; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static Result<Price> Create(decimal value)
    {
        if (value < 0)
            return Result.Failure<Price>(TicketErrors.InvalidPrice);

        return Result.Success(new Price(value));
    }
}
