using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Queries.GetById;

public class GetReservationByIdQueryHandler : BaseQueryHandler<GetReservationByIdQuery, ReservationDto>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler(
        IRequestValidator<GetReservationByIdQuery> validator,
        IReservationRepository reservationRepository ) : base( validator )
    {
        _reservationRepository = reservationRepository;
    }

    protected override async Task<Result<ReservationDto>> HandleQuery( GetReservationByIdQuery query, CancellationToken cancellationToken )
    {
        Reservation existingReservation = await _reservationRepository.GetById( query.ReservationId );
        ReservationDto reservation = existingReservation.Adapt<ReservationDto>();

        return Result<ReservationDto>.Success( reservation );
    }
}
