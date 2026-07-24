using CleanArquitecture.Domain.Entities.Customer;

namespace CleanArquitecture.Application.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken(Customer customer);
}
