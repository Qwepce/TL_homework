using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IReservationRepository :
    ICreateEntityRepository<Reservation>,
    IDeleteEntityRepository<Reservation>
{
    Task<Reservation?> GetById( int reservationId );

    Task<IReadOnlyList<Reservation>> GetAll( SearchReservationsFilter filter );

    Task<int> GetOverlappingReservationsCount(
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate );
}
