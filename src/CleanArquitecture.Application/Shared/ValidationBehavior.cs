using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Exceptions;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CleanArquitecture.Application.Shared;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                       where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            await next();

        var context = new ValidationContext<TRequest>(request);

        var validationsErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (validationsErrors.Any())
            throw new Exceptions.ValidationException(validationsErrors);

        return await next();
    }
}
