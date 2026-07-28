using CleanArquitecture.Application.Producer.CreateProducer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.Controllers.Producer.CreateProducer;

[Route("api/create-producer")]
[ApiController]
public class CreateProducerController : ControllerBase
{
    private ISender _sender;

    public CreateProducerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProducerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProducerCommand(
            request.Name,
            request.Password,
            request.Email,
            request.Cnpj,
            request.Description);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
