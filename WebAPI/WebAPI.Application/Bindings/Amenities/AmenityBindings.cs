using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.UseCases.Amenities.GetOrCreate;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Bindings.Amenities;

public static class AmenityBindings
{
    public static IServiceCollection AddAmenityBindings( this IServiceCollection services )
    {
        services.AddScoped<ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>>, GetOrCreateAmenitiesCommandHandler>();

        return services;
    }
}
