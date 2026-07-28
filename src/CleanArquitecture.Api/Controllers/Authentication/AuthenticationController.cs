using CleanArquitecture.Application.Shared.Authenticate.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.Controllers.Authentication;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate(AuthenticationRequest request, CancellationToken cancellationToken)
    {
        var authenticate = new AuthenticateCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(authenticate, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error); 
    }
}
