using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetById;

public class GetRoomTypeByIdQueryHandler : BaseQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>
{
    private readonly IRoomTypeRepository _roomTypeRepository;

    public GetRoomTypeByIdQueryHandler(
        IRoomTypeRepository roomTypeRepository,
        IRequestValidator<GetRoomTypeByIdQuery> validator )
        : base( validator )
    {
        _roomTypeRepository = roomTypeRepository;
    }

    protected override async Task<Result<RoomTypeDto>> HandleQuery( GetRoomTypeByIdQuery query, CancellationToken cancellationToken )
    {
        RoomType roomType = await _roomTypeRepository.GetById( query.RoomTypeId );

        RoomTypeDto roomTypeDto = roomType.Adapt<RoomTypeDto>();

        return Result<RoomTypeDto>.Success( roomTypeDto );
    }
}
