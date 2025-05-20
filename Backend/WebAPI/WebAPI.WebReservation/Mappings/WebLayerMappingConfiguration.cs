using WebAPI.WebReservation.Mappings.Reservations;

namespace WebAPI.WebReservation.Mappings;

public static class WebLayerMappingConfiguration
{
    public static IServiceCollection AddWebMappingBindings( this IServiceCollection services )
    {
        ReservationMappingConfiguration.AddEntityMapping();

        return services;
    }
}
