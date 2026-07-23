using CleanArquitecture.Domain.Abstrations;
using MediatR;

namespace CleanArquitecture.Application.Abstrations.Messaging;

public interface IQuery : IRequest<Result>
{

}

public interface IQuery<TResponse> : IRequest<Result<TResponse>> 
{

}

