using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Queries.GetById;

public class GetRoomTypeByIdQueryValidator : IRequestValidator<GetRoomTypeByIdQuery>
{
    private readonly IRoomTypeRepository _roomTypeRepository;

    public GetRoomTypeByIdQueryValidator( IRoomTypeRepository roomTypeRepository )
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result> Validate( GetRoomTypeByIdQuery query )
    {
        RoomType roomType = await _roomTypeRepository.GetById( query.RoomTypeId );
        if ( roomType is null )
        {
            return Result.Failure( new Error( $"Room type with ID: {query.RoomTypeId} was not found!" ) );
        }

        return Result.Success();
    }
}
