using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.UseCases.Amenities;
using WebAPI.Application.UseCases.Properties;
using WebAPI.Application.UseCases.RoomServices;
using WebAPI.Application.UseCases.RoomTypes;

namespace WebAPI.Application;

public static class ApplicationLayerBindings
{
    public static IServiceCollection AddApplicationLayerBindings( this IServiceCollection services )
    {
        services.AddPropertyBindings();
        services.AddRoomTypeBindings();
        services.AddAmenityBindings();
        services.AddRoomServiceBindings();

        return services;
    }
}
