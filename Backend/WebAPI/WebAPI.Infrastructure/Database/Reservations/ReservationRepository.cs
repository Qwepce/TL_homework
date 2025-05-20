using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Reservations;

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
            .ApplySearchFilters( filter )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetReservationsCountByCategoryAndDates(
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate )
    {
        return await _dbContext.Reservations
            .CountAsync( r =>
                r.RoomTypeId == roomTypeId &&
                r.ArrivalDate < departureDate &&
                r.DepartureDate > arrivalDate );
    }

    public async Task<Reservation> GetById( int reservationId )
    {
        return await _dbContext.Reservations
            .FirstOrDefaultAsync( r => r.Id == reservationId );
    }

    public async Task Add( Reservation reservation )
    {
        await _dbContext.AddAsync( reservation );
    }

    public async Task Delete( Reservation reservation )
    {
        Reservation existingReservation = await GetById( reservation.Id );
        if ( existingReservation is not null )
        {
            _dbContext.Reservations.Remove( reservation );
        }
    }

    public async Task<bool> IsPropertyUsedInReservations( int propertyId )
    {
        return await _dbContext.Reservations
            .AnyAsync( r => r.PropertyId == propertyId );
    }

    public async Task<bool> IsRoomTypeUsedInReservations( int roomTypeId )
    {
        return await _dbContext.Reservations
            .AnyAsync( r => r.RoomTypeId == roomTypeId );
    }
}
