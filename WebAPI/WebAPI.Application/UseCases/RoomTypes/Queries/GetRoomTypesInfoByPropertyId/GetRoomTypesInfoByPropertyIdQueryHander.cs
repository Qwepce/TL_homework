using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetRoomTypesInfoByPropertyId;

public class GetRoomTypesInfoByPropertyIdQueryHander : BaseQueryHandler<GetRoomTypesInfoByPropertyIdQuery, IReadOnlyList<RoomTypeDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public GetRoomTypesInfoByPropertyIdQueryHander(
        IRoomTypeRepository roomTypeRepository,
        IPropertyRepository implicitPropertyRepository,
        IRequestValidator<GetRoomTypesInfoByPropertyIdQuery> validator )
        : base( validator )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = implicitPropertyRepository;
    }

    protected override async Task<Result<IReadOnlyList<RoomTypeDto>>> HandleQuery( GetRoomTypesInfoByPropertyIdQuery query, CancellationToken cancellationToken )
    {
        Property existedProperty = await _propertyRepository.GetById( query.PropertyId );
        if ( existedProperty is null )
        {
            return Result<IReadOnlyList<RoomTypeDto>>.Failure( new Error( $"Property with ID: {query.PropertyId} was not found" ) );
        }

        IReadOnlyCollection<RoomType> roomTypes = await _roomTypeRepository.GetAllRoomTypesInfoByPropertyId( existedProperty.Id );
        IReadOnlyList<RoomTypeDto> roomTypeDtos = roomTypes.Adapt<IReadOnlyList<RoomTypeDto>>();

        return Result<IReadOnlyList<RoomTypeDto>>.Success( roomTypeDtos );
    }
}
