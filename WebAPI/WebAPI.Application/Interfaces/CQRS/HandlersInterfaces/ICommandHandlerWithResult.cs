using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;

public interface ICommandHandlerWithResult<in TCommand, TResult>
    where TCommand : class
{
    Task<Result<TResult>> Handle( TCommand command, CancellationToken cancellationToken );
}