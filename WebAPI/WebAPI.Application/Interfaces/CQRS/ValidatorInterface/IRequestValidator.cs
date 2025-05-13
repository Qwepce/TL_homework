using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.Interfaces.CQRS.ValidatorInterface;

public interface IRequestValidator<in TRequest> where TRequest : class
{
    Task<Result> Validate( TRequest request );
}
