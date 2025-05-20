using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetByPropertyIdQuery;

public class GetRoomTypesInfoByPropertyIdQueryValidator : IRequestValidator<GetRoomTypesInfoByPropertyIdQuery>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetRoomTypesInfoByPropertyIdQueryValidator( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result> Validate( GetRoomTypesInfoByPropertyIdQuery query )
    {
        Property property = await _propertyRepository.GetById( query.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {query.PropertyId} was not found" ) );
        }

        return Result.Success();
    }
}
