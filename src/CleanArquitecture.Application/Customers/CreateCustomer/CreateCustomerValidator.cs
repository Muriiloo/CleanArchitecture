using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using FluentValidation;

namespace CleanArquitecture.Application.Customers.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FullName)
            .Must(fullName => FullName.Create(fullName).IsSuccess)
            .WithMessage("Name is not valid.");

        RuleFor(x => x.Password)
            .Must(password => Password.Create(password).IsSuccess)
            .WithMessage("Password is not valid.");

        RuleFor(x => x.Email)
            .Must(email => Email.Create(email).IsSuccess)
            .WithMessage("Email is not valid.");

        RuleFor(x => x.Cpf)
            .Must(cpf => Cpf.Create(cpf).IsSuccess)
            .WithMessage("Cpf is not valid.");

        RuleFor(x => x.BirthDay)
            .Must(birthDay => BirthDay.Create(birthDay).IsSuccess)
            .WithMessage("Unauthorized age.");
    }
}
