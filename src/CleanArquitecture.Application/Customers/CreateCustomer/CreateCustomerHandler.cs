using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;

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

        var customer = Customer.Create(
            fullName.Value,
            password.Value,
            email.Value,
            cpf.Value,
            birthDay.Value);

            _customerRepo.Add(customer.Value);

        var customerId = customer.Value.Id.Value;

        try
        {
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(customerId);
        }
        catch
        {
            throw;
        }
    }
}
