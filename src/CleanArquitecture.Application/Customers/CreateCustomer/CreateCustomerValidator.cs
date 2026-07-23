using FluentValidation;

namespace CleanArquitecture.Application.Customers.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Cpf).NotEmpty().MaximumLength(11);
        RuleFor(x => x.BirthDay).Must(date => date >= new DateOnly(1900, 1, 1) && date <= DateOnly.FromDateTime(DateTime.Today)).WithMessage("A data de nascimento precisar ser maior que 1900 e menor que o ano atual.");
    }
}
