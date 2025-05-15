using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : class
    where TResult : class
{
    Task<Result<TResult>> Handle( TQuery query, CancellationToken cancellationToken );
}
