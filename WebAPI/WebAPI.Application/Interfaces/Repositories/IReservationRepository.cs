using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IReservationRepository :
    IAddEntityRepository<Reservation>,
    IDeleteEntityRepository<Reservation>,
    IGetEntityByIdRepository<Reservation>
{
    Task<IReadOnlyList<Reservation>> GetAll( SearchReservationsFilter filter );

    Task<int> GetReservationsCountByCategoryAndDates(
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate );

    Task<bool> IsPropertyUsedInReservations( int propertyId );

    Task<bool> IsRoomTypeUsedInReservations( int roomTypeId );
}
