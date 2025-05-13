using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.DeleteRoomType;

public class DeleteRoomTypeByIdCommandValidator : IRequestValidator<DeleteRoomTypeByIdCommand>
{
    private readonly IRoomTypeRepository _roomTypeRepository;

    public DeleteRoomTypeByIdCommandValidator( IRoomTypeRepository roomTypeRepository )
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result> Validate( DeleteRoomTypeByIdCommand command )
    {
        RoomType roomType = await _roomTypeRepository.GetById( command.RoomTypeId );
        if ( roomType is null )
        {
            return Result.Failure( new Error( $"Room type with ID: {command.RoomTypeId} was not found!" ) );
        }

        return Result.Success();
    }
}
