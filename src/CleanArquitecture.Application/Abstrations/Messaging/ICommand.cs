using CleanArquitecture.Domain.Abstrations;
using MediatR;

namespace CleanArquitecture.Application.Abstrations.Messaging;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{

}
