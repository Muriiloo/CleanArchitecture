using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;

namespace CleanArquitecture.Application.Customers.CreateCustomer;

public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
    {
        _customerRepo = customerRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FullName);
        var password = Password.Create(request.Password);
        var email = Email.Create(request.Email);
        var cpf = Cpf.Create(request.Cpf);
        var birthDay = BirthDay.Create(request.BirthDay);

        var errors = ValidationErrorMapper.GetValidationErrors(fullName, password, email, cpf, birthDay);

        if (errors.Any())
            throw new ValidationException(errors);

        List<Error> error = [];

        var emailExists = await _customerRepo.GetCustomerByEmail(email.Value);

        if (emailExists is not null)
            error.Add(GlobalErrors.EmailAlreadyExists);

        var cpfExists = await _customerRepo.GetCustomerByCpf(cpf.Value);

        if (cpfExists is not null)
            error.Add(CustomerErrors.CpfAlreadyExists);

        if (error.Any())
            return Result.Failures<Guid>(error);

        var customer = Customer.Create(
            fullName.Value,
            password.Value,
            email.Value,
            cpf.Value,
            birthDay.Value);

        if (customer.IsFailure)
            return Result.Failure<Guid>(customer.Error);

        _customerRepo.Add(customer.Value);

        var customerId = customer.Value.Id.Value;

        await _unitOfWork.SaveChangesAsync();
        return Result.Success(customerId);
    }
}
