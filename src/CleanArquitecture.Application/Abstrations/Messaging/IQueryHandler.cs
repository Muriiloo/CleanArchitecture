using CleanArquitecture.Domain.Abstrations;
using MediatR;

namespace CleanArquitecture.Application.Abstrations.Messaging;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Result> 
    where TQuery : IQuery
{

}

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{

}
