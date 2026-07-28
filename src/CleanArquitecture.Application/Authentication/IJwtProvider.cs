using CleanArquitecture.Domain.Entities.Customer;

namespace CleanArquitecture.Application.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken(Guid id, string name, string email);
}
