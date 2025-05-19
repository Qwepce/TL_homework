using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.Amenities.GetOrCreateAmenities;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Amenities;

public static class AmenityBindings
{
    public static IServiceCollection AddAmenityBindings( this IServiceCollection services )
    {
        services.AddScoped<ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>>, GetOrCreateAmenitiesCommandHandler>();
        services.AddScoped<IRequestValidator<GetOrCreateAmenitiesCommand>, GetOrCreateAmenitiesCommandValidator>();

        return services;
    }
}
