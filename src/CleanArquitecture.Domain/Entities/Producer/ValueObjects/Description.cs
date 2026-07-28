using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Producer.ValueObjects;

public record Description
{
    string Value { get; }
    
    public Description(string value)
    {
        Value = value;
    }

    public static Result<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Description>(ProducerErrors.InvalidDescription);
        
        return Result.Success(new Description(value));
    }
}