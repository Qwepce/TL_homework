using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Queries.GetById;

public class GetReservationByIdQueryValidator : IRequestValidator<GetReservationByIdQuery>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryValidator( IReservationRepository reservationRepository )
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result> Validate( GetReservationByIdQuery query )
    {
        Reservation reservation = await _reservationRepository.GetById( query.ReservationId );
        if ( reservation is null )
        {
            return Result.Failure( new Error( $"Reservation with ID: {query.ReservationId} was not found" ) );
        }

        return Result.Success();
    }
}
