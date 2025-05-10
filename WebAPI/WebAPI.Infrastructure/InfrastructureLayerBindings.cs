using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Infrastructure.Database;
using WebAPI.Infrastructure.Database.Repositories;

namespace WebAPI.Infrastructure;

public static class InfrastructureLayerBindings
{
    public static IServiceCollection AddInfrastructureLayerBindings( this IServiceCollection services )
    {
        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
