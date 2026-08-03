using CleanArquitecture.Application.Customers.AuthenticateCustomer;
using CleanArquitecture.Application.Producer.AuthenticateProducer;
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

    [HttpPost("customer")]
    public async Task<IActionResult> AuthenticateCustomer(AuthenticationCustomerRequest request, CancellationToken cancellationToken)
    {
        var authenticate = new AuthenticateCustomerCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(authenticate, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error); 
    }

    [HttpPost("producer")]
    public async Task<IActionResult> AuthenticateProducer(AuthenticationProducerRequest request, CancellationToken cancellationToken)
    {
        var authenticate = new AuthenticateProducerCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(authenticate, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error);
    }
}
