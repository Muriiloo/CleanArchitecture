using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Authentication;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;

namespace CleanArquitecture.Application.Producer.AuthenticateProducer;

public class AuthenticateProducerHandler : ICommandHandler<AuthenticateProducerCommand, string>
{
    private readonly IProducerRepository _producerRepo;
    private readonly IJwtProvider _jwtProvider;

    public AuthenticateProducerHandler(IProducerRepository producerRepo, IJwtProvider jwtProvider)
    {
        _producerRepo = producerRepo;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<string>> Handle(AuthenticateProducerCommand request, CancellationToken cancellationToken)
    {
        var resultEmail = Email.Create(request.Email);
        var resultPassword = Password.Create(request.Password);

        var error = ValidationErrorMapper.GetValidationErrors(resultEmail, resultPassword);

        if (error.Any())
            return Result.Failure<string>(GlobalErrors.Unauthorized);

        var producer = await _producerRepo.GetByEmailAsync(resultEmail.Value, cancellationToken);

        if (producer is null)
            return Result.Failure<string>(GlobalErrors.Unauthorized);

        if(resultPassword.Value != producer.Password)
            return Result.Failure<string>(GlobalErrors.Unauthorized);

        var token = _jwtProvider.GenerateAccessToken(producer.Id.Value, producer.Name.Value, producer.Email.Value);

        return Result.Success(token);
    }
}
