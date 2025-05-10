using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.MappingConfigurations.RoomTypes;
using WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Commands.DeleteRoomType;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetById;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetByPropertyId;

namespace WebAPI.Application.Bindings.RoomTypes;

public static class RoomTypeBindings
{
    public static IServiceCollection AddRoomTypeBindings( this IServiceCollection services )
    {

        services.AddScoped<IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>, GetRoomTypeByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetRoomTypesByPropertyIdQuery, IReadOnlyList<RoomTypeDto>>, GetRoomTypesByPropertyIdQueryHander>();
        services.AddScoped<ICommandHandlerWithResult<CreateRoomTypeCommand, int>, CreateRoomTypeCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteRoomTypeByIdCommand>, DeleteRoomTypeByIdCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateRoomTypeCommand>, UpdateRoomTypeCommandHandler>();

        RoomTypeMappingConfiguration.AddEntityMapping();

        return services;
    }
}
