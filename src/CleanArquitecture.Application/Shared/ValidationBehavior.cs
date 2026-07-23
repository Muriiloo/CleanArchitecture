using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArquitecture.Application.Shared;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                       where TRequest : IBaseCommand
{
    // todos os validadores para esse request
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //se não existir nenhum validador para o request segue normalmente
        if (!_validators.Any())
            return await next();

        //contexto de validação que vai ser executado as regras definidas nos validators
        var context = new ValidationContext<TRequest>(request);

        // transformando os erros em uma unica lista
        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validatorResult => validatorResult.Errors.Any())
            .SelectMany(validatorResult => validatorResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();
            
        //se existir erro interrompe e lança uma exceção e não vai executar o handler
        if (validationErrors.Any())
            throw new Exceptions.ValidationException(validationErrors);

        //se tudo estiver ok continua o pipeline, nessa parte mediatr chama o prox comportamento ou handler caso não exista outro comportamento
        return await next();
    }
}
