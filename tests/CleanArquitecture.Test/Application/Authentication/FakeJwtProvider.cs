using CleanArquitecture.Application.Authentication;

namespace CleanArquitecture.Test.Application.Authentication;

public class FakeJwtProvider : IJwtProvider
{
    public string GenerateAccessToken(Guid id, string name, string email) => "fake-jwt-token";
}
