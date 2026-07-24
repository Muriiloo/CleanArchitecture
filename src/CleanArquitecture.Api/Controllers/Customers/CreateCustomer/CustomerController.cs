using CleanArquitecture.Application.Customers.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.Controllers.Customers.CreateCustomer;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ISender _sender;
    public CustomerController(ISender sender)
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
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
