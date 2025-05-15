using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.BaseValidator;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandValidator : BaseRoomTypeCommandsValidator<UpdateRoomTypeCommand>
{
    private readonly IRoomTypeRepository _roomTypeRepository;

    public UpdateRoomTypeCommandValidator( IRoomTypeRepository roomTypeRepository )
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public override async Task<Result> Validate( UpdateRoomTypeCommand command )
    {
        RoomType roomType = await _roomTypeRepository.GetById( command.RoomTypeId );
        if ( roomType is null )
        {
            return Result.Failure( new Error( $"Room type with ID: {command.RoomTypeId} was not found" ) );
        }

        return await base.Validate( command );
    }
}
