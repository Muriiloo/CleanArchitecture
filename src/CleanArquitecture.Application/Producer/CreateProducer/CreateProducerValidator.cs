using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using FluentValidation;

namespace CleanArquitecture.Application.Producer.CreateProducer;

public class CreateProducerValidator : AbstractValidator<CreateProducerCommand>
{
    public CreateProducerValidator()
    {
        RuleFor(x => x.Name)
            .Must(name => Name.Create(name).IsSuccess)
            .WithMessage("Invalid name");

        RuleFor(x => x.Description)
            .Must(description => Description.Create(description).IsSuccess)
            .WithMessage("Invalid description");

        RuleFor(x => x.Cnpj)
            .Must(cnpj => Cnpj.Create(cnpj).IsSuccess)
            .WithMessage("Invalid CNPJ");

        RuleFor(x => x.Email)
            .Must(email => Email.Create(email).IsSuccess)
            .WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
            .Must(password => Password.Create(password).IsSuccess)
            .WithMessage("Password is not valid.");
    }
}
