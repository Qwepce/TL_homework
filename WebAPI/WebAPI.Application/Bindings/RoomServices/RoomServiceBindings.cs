using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.UseCases.RoomServices.GetOrCreate;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Bindings.RoomServices;

public static class RoomServiceBindings
{
    public static IServiceCollection AddRoomServiceBindings( this IServiceCollection services )
    {
        services.AddScoped<ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>>, GetOrCreateRoomServicesCommandHandler>();

        return services;
    }
}
