using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;

public interface ICommandHandler<in TCommand> where TCommand : class
{
    Task<Result> Handle( TCommand command, CancellationToken cancellationToken );
}