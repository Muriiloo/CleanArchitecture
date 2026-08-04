using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using FluentValidation;

namespace CleanArquitecture.Application.Customers.AuthenticateCustomer;

public class AuthenticateCustomerValidator : AbstractValidator<AuthenticateCustomerCommand>
{
    public AuthenticateCustomerValidator()
    {
        RuleFor(x => x.Email)
            .Must(email => Email.Create(email).IsSuccess)
            .WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
            .Must(password => Password.Create(password).IsSuccess)
            .WithMessage("Password is not valid.");
    }
}
