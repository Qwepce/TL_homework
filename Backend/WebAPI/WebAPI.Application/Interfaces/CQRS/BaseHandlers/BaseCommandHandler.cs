using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.BaseHandlers;

public abstract class BaseCommandHandler<T> : ICommandHandler<T>
    where T : class
{
    private readonly IRequestValidator<T> _validator;

    protected BaseCommandHandler( IRequestValidator<T> validator )
    {
        _validator = validator;
    }

    public virtual async Task<Result> Handle( T command, CancellationToken cancellationToken )
    {
        Result validationResult = await _validator.Validate( command );
        if ( validationResult.IsFailure )
        {
            return Result.Failure( validationResult.Errors );
        }

        return await HandleCommand( command, cancellationToken );
    }

    protected abstract Task<Result> HandleCommand( T command, CancellationToken cancellationToken );
}