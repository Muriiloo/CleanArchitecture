using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Authentication;
using CleanArquitecture.Application.Shared.Authenticate.Command;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;

namespace CleanArquitecture.Application.Customers.AuthenticateCustomer;

public class AuthenticateCustomerHandler : ICommandHandler<AuthenticateCommand, string>
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IJwtProvider _jwtProvider;

    public AuthenticateCustomerHandler(ICustomerRepository customerRepo, IJwtProvider jwtProvider)
    {
        _customerRepo = customerRepo;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var password = Password.Create(request.Password);

        if (email.IsFailure)
            return Result.Failure<string>(email.Error);

        if (password.IsFailure)
            return Result.Failure<string>(password.Error);

        var customer = await _customerRepo.GetCustomerByEmail(email.Value, cancellationToken);

        if (customer is null)
            return Result.Failure<string>(CustomerErrors.NotFound);

        if (customer.Password.Value != password.Value.Value)
            return Result.Failure<string>(GlobalErrors.Unauthorized);

        var token = _jwtProvider.GenerateAccessToken(customer.Id.Value, customer.FullName.Value, customer.Email.Value);

        return Result.Success(token);
    }
}
