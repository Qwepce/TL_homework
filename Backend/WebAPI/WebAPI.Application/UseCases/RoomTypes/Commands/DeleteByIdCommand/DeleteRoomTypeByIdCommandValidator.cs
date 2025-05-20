using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.DeleteByIdCommand;

public class DeleteRoomTypeByIdCommandValidator : IRequestValidator<DeleteRoomTypeByIdCommand>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public DeleteRoomTypeByIdCommandValidator( IRoomTypeRepository roomTypeRepository, IReservationRepository reservationRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<Result> Validate( DeleteRoomTypeByIdCommand command )
    {
        RoomType roomType = await _roomTypeRepository.GetById( command.RoomTypeId );
        if ( roomType is null )
        {
            return Result.Failure( new Error( $"Room type with ID: {command.RoomTypeId} was not found!" ) );
        }

        bool isRoomTypeUsedInReservations = await _reservationRepository.IsRoomTypeUsedInReservations( command.RoomTypeId );
        if ( isRoomTypeUsedInReservations )
        {
            return Result.Failure( new Error( $"Room type with ID: {command.RoomTypeId} used in existing reservations" ) );
        }

        return Result.Success();
    }
}
