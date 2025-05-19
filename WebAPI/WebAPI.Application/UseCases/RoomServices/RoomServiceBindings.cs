using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.RoomServices.GetOrCreateRoomServices;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomServices;

public static class RoomServiceBindings
{
    public static IServiceCollection AddRoomServiceBindings( this IServiceCollection services )
    {
        services.AddScoped<ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>>, GetOrCreateRoomServicesCommandHandler>();
        services.AddScoped<IRequestValidator<GetOrCreateRoomServicesCommand>, GetOrCreateRoomServicesCommandValidator>();

        return services;
    }
}
