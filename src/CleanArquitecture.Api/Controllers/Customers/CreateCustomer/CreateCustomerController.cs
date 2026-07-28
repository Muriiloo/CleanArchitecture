using CleanArquitecture.Application.Customers.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.Controllers.Customers.CreateCustomer;

[Route("api/create-customer")]
[ApiController]
public class CreateCustomerController : ControllerBase
{
    private readonly ISender _sender;
    public CreateCustomerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(
            request.FullName,
            request.Password,
            request.Email,
            request.Cpf,
            request.BirthDay);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
