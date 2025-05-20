using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.BaseHandlers;

public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : class
    where TResult : class
{
    private readonly IRequestValidator<TQuery> _validator;

    protected BaseQueryHandler( IRequestValidator<TQuery> validator )
    {
        _validator = validator;
    }

    public async Task<Result<TResult>> Handle( TQuery query, CancellationToken cancellationToken )
    {
        Result validationResult = await _validator.Validate( query );
        if ( validationResult.IsFailure )
        {
            return Result<TResult>.Failure( validationResult.Errors );
        }

        return await HandleQuery( query, cancellationToken );
    }

    protected abstract Task<Result<TResult>> HandleQuery( TQuery query, CancellationToken cancellationToken );
}