using Mapster;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Queries.GetAll;

public class GetAllReservationsQueryHandler : IQueryHandler<GetAllReservationsQuery, List<ReservationDto>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetAllReservationsQueryHandler( IReservationRepository reservationRepository )
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<List<ReservationDto>>> Handle( GetAllReservationsQuery query, CancellationToken cancellationToken )
    {
        SearchReservationsFilter filter = query.Adapt<SearchReservationsFilter>();
        IReadOnlyList<Reservation> reservations = await _reservationRepository.GetAll( filter );

        List<ReservationDto> reservationsDto = reservations.Adapt<List<ReservationDto>>();

        return Result<List<ReservationDto>>.Success( reservationsDto );
    }
}
