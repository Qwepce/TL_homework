using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Extensions;

public static class PropertyQueryExtension
{
    public static IQueryable<Property> ApplySearchByCityFilter( this IQueryable<Property> query, string city )
    {
        if ( string.IsNullOrWhiteSpace( city ) )
        {
            return query;
        }

        return query.Where( p => p.City.Equals( city ) );
    }
}