using FluentValidation;

namespace CleanArquitecture.Application.Customers.AuthenticateCustomer;

public class AuthenticateCustomerValidator : AbstractValidator<AuthenticateCustomerCommand>
{
    public AuthenticateCustomerValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(4);
    }
}
