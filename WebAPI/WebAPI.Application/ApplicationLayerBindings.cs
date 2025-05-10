using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Bindings.Amenities;
using WebAPI.Application.Bindings.Properties;
using WebAPI.Application.Bindings.RoomServices;
using WebAPI.Application.Bindings.RoomTypes;
using WebAPI.Application.Utils;
using WebAPI.Application.Validation;

namespace WebAPI.Application;

public static class ApplicationLayerBindings
{
    public static IServiceCollection AddApplicationLayerBindings( this IServiceCollection services )
    {
        services.AddPropertyBindings();
        services.AddRoomTypeBindings();
        services.AddAmenityBindings();
        services.AddRoomServiceBindings();
        services.AddValidationBindings();
        services.AddScoped<IPriceCalculator, PriceCalculator>();

        return services;
    }
}
