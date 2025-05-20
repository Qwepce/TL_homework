using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.BaseHandlers;

public abstract class BaseCommandHandlerWithResult<TCommand, TResult> : ICommandHandlerWithResult<TCommand, TResult>
    where TCommand : class
{
    private readonly IRequestValidator<TCommand> _validator;

    protected BaseCommandHandlerWithResult( IRequestValidator<TCommand> validator )
    {
        _validator = validator;
    }

    public async Task<Result<TResult>> Handle( TCommand command, CancellationToken cancellationToken )
    {
        Result validationResult = await _validator.Validate( command );
        if ( validationResult.IsFailure )
        {
            return Result<TResult>.Failure( validationResult.Errors );
        }

        return await HandleCommand( command, cancellationToken );
    }

    protected abstract Task<Result<TResult>> HandleCommand( TCommand command, CancellationToken cancellationToken );
}