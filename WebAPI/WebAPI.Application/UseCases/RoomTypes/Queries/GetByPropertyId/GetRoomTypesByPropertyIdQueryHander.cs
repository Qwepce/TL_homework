using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetByPropertyId;

public class GetRoomTypesByPropertyIdQueryHander : IQueryHandler<GetRoomTypesByPropertyIdQuery, IReadOnlyList<RoomTypeDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public GetRoomTypesByPropertyIdQueryHander(
        IRoomTypeRepository roomTypeRepository,
        IPropertyRepository implicitPropertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = implicitPropertyRepository;
    }

    public async Task<Result<IReadOnlyList<RoomTypeDto>>> Handle( GetRoomTypesByPropertyIdQuery query, CancellationToken cancellationToken )
    {
        Property? existedProperty = await _propertyRepository.GetById( query.PropertyId );
        if ( existedProperty is null )
        {
            return Result<IReadOnlyList<RoomTypeDto>>.Failure( new Error( $"Property with ID: {query.PropertyId} was not found" ) );
        }

        IReadOnlyCollection<RoomType> roomTypes = await _roomTypeRepository.GetAllByPropertyId( existedProperty.Id );
        IReadOnlyList<RoomTypeDto> roomTypeDtos = roomTypes.Adapt<IReadOnlyList<RoomTypeDto>>();

        return Result<IReadOnlyList<RoomTypeDto>>.Success( roomTypeDtos );
    }
}
