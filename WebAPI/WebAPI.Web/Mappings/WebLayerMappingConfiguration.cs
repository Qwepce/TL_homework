using WebAPI.Application.UseCases.RoomTypes;
using WebAPI.Web.Mappings.Property;

namespace WebAPI.Web.Mappings;

public static class WebLayerMappingConfiguration
{
    public static IServiceCollection AddWebMappingBindings( this IServiceCollection services )
    {
        PropertyMappingConfiguration.AddEntityMapping();
        RoomTypeMappingConfiguration.AddEntityMapping();

        return services;
    }
}
