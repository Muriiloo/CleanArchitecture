using CleanArquitecture.Application.Shared.Authenticate.Command;
using FluentValidation;

namespace CleanArquitecture.Application.Shared.Authenticate.Validator;

public class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(4);
    }
}
