using FluentValidation;

namespace CleanArquitecture.Application.Producer.CreateProducer;

public class CreateProducerValidator : AbstractValidator<CreateProducerCommand>
{
    public CreateProducerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Cnpj).NotEmpty().WithMessage("Cnpj is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}