using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Queries.GetById;

public class GetPropertyByIdQueryHandler : IQueryHandler<GetPropertyByIdQuery, PropertyDto>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyByIdQueryHandler( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<PropertyDto>> Handle( GetPropertyByIdQuery query, CancellationToken cancellationToken )
    {
        Property? property = await _propertyRepository.GetById( query.PropertyId );
        if ( property is null )
        {
            return Result<PropertyDto>.Failure( new Error( $"Property with id {query.PropertyId} was not found!" ) );
        }

        PropertyDto propertyDto = property.Adapt<PropertyDto>();

        return Result<PropertyDto>.Success( propertyDto );
    }
}
