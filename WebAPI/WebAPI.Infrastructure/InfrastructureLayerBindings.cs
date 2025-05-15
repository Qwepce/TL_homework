using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Infrastructure.Database;
using WebAPI.Infrastructure.Database.Amenities;
using WebAPI.Infrastructure.Database.Properties;
using WebAPI.Infrastructure.Database.Reservations;
using WebAPI.Infrastructure.Database.RoomServices;
using WebAPI.Infrastructure.Database.RoomTypes;
using WebAPI.Infrastructure.Database.UnitOfWork;

namespace WebAPI.Infrastructure;

public static class InfrastructureLayerBindings
{
    public static IServiceCollection AddInfrastructureLayerBindings( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<ApplicationDbContext>( options =>
        {
            string connectionStringName = configuration.GetConnectionString( "MsSQL" );
            options.UseSqlServer( connectionStringName );
        } );

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
