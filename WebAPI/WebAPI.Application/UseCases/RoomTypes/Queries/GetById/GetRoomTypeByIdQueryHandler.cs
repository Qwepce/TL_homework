using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetById;

public class GetRoomTypeByIdQueryHandler : IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>
{
    private readonly IRoomTypeRepository _roomTypeRepository;

    public GetRoomTypeByIdQueryHandler( IRoomTypeRepository roomTypeRepository )
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result<RoomTypeDto>> Handle( GetRoomTypeByIdQuery query, CancellationToken cancellationToken )
    {
        RoomType? roomType = await _roomTypeRepository.GetById( query.RoomTypeId );
        if ( roomType is null )
        {
            return Result<RoomTypeDto>.Failure( new Error( $"Room type with id {query.RoomTypeId} was not found" ) );
        }

        RoomTypeDto roomTypeDto = roomType.Adapt<RoomTypeDto>();

        return Result<RoomTypeDto>.Success( roomTypeDto );
    }
}
