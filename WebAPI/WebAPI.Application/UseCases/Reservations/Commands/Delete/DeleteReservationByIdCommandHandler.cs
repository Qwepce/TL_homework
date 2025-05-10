using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Commands.Delete;

public class DeleteReservationByIdCommandHandler : ICommandHandler<DeleteReservationByIdCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationByIdCommandHandler(
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork,
        IRoomTypeRepository roomTypeRepository )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result> Handle( DeleteReservationByIdCommand command, CancellationToken cancellationToken )
    {
        Reservation? existingReservation = await _reservationRepository.GetById( command.ReservationId );

        if ( existingReservation is null )
        {
            return Result.Failure( new Error( $"Reservation with id {command.ReservationId} was not found" ) );
        }

        RoomType existingRoomType = ( await _roomTypeRepository.GetById( existingReservation.RoomTypeId ) )!;

        await _reservationRepository.Delete( existingReservation );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}
