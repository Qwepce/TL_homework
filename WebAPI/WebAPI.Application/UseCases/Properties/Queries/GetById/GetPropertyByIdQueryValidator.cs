using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Queries.GetById;

public class GetPropertyByIdQueryValidator : IRequestValidator<GetPropertyByIdQuery>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyByIdQueryValidator( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result> Validate( GetPropertyByIdQuery query )
    {
        Property property = await _propertyRepository.GetById( query.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {query.PropertyId} was not found" ) );
        }

        return Result.Success();
    }
}
