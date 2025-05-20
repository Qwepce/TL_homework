using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Commands.DeleteByIdCommand;

public class DeleteReservationByIdCommandValidator : IRequestValidator<DeleteReservationByIdCommand>
{
    private readonly IReservationRepository _reservationRepository;

    public DeleteReservationByIdCommandValidator( IReservationRepository reservationRepository )
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result> Validate( DeleteReservationByIdCommand command )
    {
        Reservation reservation = await _reservationRepository.GetById( command.ReservationId );
        if ( reservation is null )
        {
            return Result.Failure( new Error( $"Reservation with ID: {command.ReservationId} was not found" ) );
        }

        return Result.Success();
    }
}
