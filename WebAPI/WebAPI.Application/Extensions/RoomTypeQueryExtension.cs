using WebAPI.Application.Filters;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Extensions;

public static class RoomTypeQueryExtension
{
    public static IQueryable<RoomType> ApplySearchFilters(
        this IQueryable<RoomType> query,
        SearchRoomTypesFilter filter )
    {
        if ( filter is null )
        {
            return query;
        }

        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( rt => rt.PropertyId == filter.PropertyId );
        }

        if ( filter.GuestsCount.HasValue )
        {
            query = query.Where( rt => rt.MinPersonCount <= filter.GuestsCount && rt.MaxPersonCount >= filter.GuestsCount );
        }

        return query;
    }
}
