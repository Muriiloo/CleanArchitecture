using FluentValidation;

namespace CleanArquitecture.Application.Producer.AuthenticateProducer;

public class AuthenticateProducerValidator : AbstractValidator<AuthenticateProducerCommand>
{
    public AuthenticateProducerValidator()
    {
        RuleFor(p => p.Email).NotEmpty().MinimumLength(3);
        RuleFor(p => p.Password).NotEmpty().MinimumLength(3);
    }
}
