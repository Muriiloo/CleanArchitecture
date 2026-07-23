using CleanArquitecture.Domain.Abstrations;
using System.Net.Mail;

namespace CleanArquitecture.Domain.Entities.Customer.ValueObjects;

public record Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>(CustomerErrors.InvalidEmail);

        try
        {
            var result = new MailAddress(email);
            if (result.Address != email)
                return Result.Failure<Email>(CustomerErrors.InvalidEmail);

            return Result.Success<Email>(new Email(email));
        }
        catch (FormatException)
        {
            return Result.Failure<Email>(CustomerErrors.InvalidEmail);
        }
    }
}
