using WebAPI.Application.Filters;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Extensions;

public static class ReservationQueryExtension
{
    public static IQueryable<Reservation> ApplySearchFilters(
        this IQueryable<Reservation> query,
        SearchReservationsFilter filter )
    {
        if ( filter is null )
        {
            return query;
        }

        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == filter.PropertyId.Value );
        }

        if ( filter.RoomTypeId.HasValue )
        {
            query = query.Where( r => r.RoomTypeId == filter.RoomTypeId.Value );
        }

        if ( filter.ArrivalDate.HasValue )
        {
            query = query.Where( r => r.ArrivalDate >= filter.ArrivalDate.Value );
        }

        if ( filter.DepartureDate.HasValue )
        {
            query = query.Where( r => r.DepartureDate <= filter.DepartureDate.Value );
        }

        if ( !string.IsNullOrWhiteSpace( filter.GuestName ) )
        {
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );
        }

        if ( !string.IsNullOrWhiteSpace( filter.GuestPhoneNumber ) )
        {
            query = query.Where( r => r.GuestPhoneNumber.Contains( filter.GuestPhoneNumber ) );
        }

        if ( !string.IsNullOrWhiteSpace( filter.ReservationCurrency ) )
        {
            query = query.Where( r => r.ReservationCurrency.ToString() == filter.ReservationCurrency );
        }

        return query;
    }
}
