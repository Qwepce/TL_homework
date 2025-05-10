using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Queries.GetById;

public class GetReservationByIdQueryHandler : IQueryHandler<GetReservationByIdQuery, ReservationDto>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler( IReservationRepository reservationRepository )
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<ReservationDto>> Handle( GetReservationByIdQuery query, CancellationToken cancellationToken )
    {
        Reservation? existingReservation = await _reservationRepository.GetById( query.ReservationId );
        if ( existingReservation is null )
        {
            return Result<ReservationDto>.Failure( new Error( $"Reservation with id {query.ReservationId} was not found" ) );
        }

        ReservationDto reservation = existingReservation.Adapt<ReservationDto>();

        return Result<ReservationDto>.Success( reservation );
    }
}
