using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<Reservation>> GetAll( SearchReservationsFilter filter )
    {
        return await _dbContext.Reservations
            .WhereIf( filter.PropertyId.HasValue, r => r.PropertyId == filter.PropertyId )
            .WhereIf( filter.RoomTypeId.HasValue, r => r.RoomTypeId == filter.RoomTypeId )
            .WhereIf( filter.ArrivalDate.HasValue, r => r.ArrivalDate >= filter.ArrivalDate )
            .WhereIf( filter.DepartureDate.HasValue, r => r.DepartureDate <= filter.DepartureDate )
            .WhereIf( !string.IsNullOrWhiteSpace( filter.GuestName ), r => r.GuestName.Contains( filter.GuestName ) )
            .WhereIf( !string.IsNullOrWhiteSpace( filter.GuestPhoneNumber ), r => r.GuestPhoneNumber.Contains( filter.GuestPhoneNumber ) )
            .WhereIf( !string.IsNullOrWhiteSpace( filter.ReservationCurrency ), r => r.ReservationCurrency.ToString() == filter.ReservationCurrency )
            .ToListAsync();
    }

    public async Task<Reservation?> GetById( int reservationId )
    {
        return await _dbContext.Reservations
            .FirstOrDefaultAsync( r => r.Id == reservationId );
    }

    public async Task Create( Reservation entity )
    {
        await _dbContext.AddAsync( entity );
    }

    public async Task Delete( Reservation entity )
    {
        Reservation? existingReservation = await GetById( entity.Id );

        if ( existingReservation is not null )
        {
            _dbContext.Reservations.Remove( entity );
        }
    }

    public async Task<int> GetOverlappingReservationsCount(
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate )
    {
        return await _dbContext.Reservations
            .CountAsync( r => r.RoomTypeId == roomTypeId &&
                            r.ArrivalDate < departureDate &&
                            r.DepartureDate > arrivalDate );
    }
}
