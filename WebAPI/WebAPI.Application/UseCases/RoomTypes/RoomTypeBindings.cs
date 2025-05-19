using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Commands.DeleteRoomTypeById;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetRoomTypeById;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetRoomTypesInfoByPropertyId;

namespace WebAPI.Application.UseCases.RoomTypes;

public static class RoomTypeBindings
{
    public static IServiceCollection AddRoomTypeBindings( this IServiceCollection services )
    {

        services.AddScoped<IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>, GetRoomTypeByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetRoomTypesInfoByPropertyIdQuery, IReadOnlyList<RoomTypeDto>>, GetRoomTypesInfoByPropertyIdQueryHander>();
        services.AddScoped<ICommandHandler<DeleteRoomTypeByIdCommand>, DeleteRoomTypeByIdCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateRoomTypeCommand>, UpdateRoomTypeCommandHandler>();
        services.AddScoped<ICommandHandlerWithResult<CreateRoomTypeCommand, int>, CreateRoomTypeCommandHandler>();

        services.AddScoped<IRequestValidator<CreateRoomTypeCommand>, CreateRoomTypeCommandValidator>();
        services.AddScoped<IRequestValidator<UpdateRoomTypeCommand>, UpdateRoomTypeCommandValidator>();
        services.AddScoped<IRequestValidator<GetRoomTypeByIdQuery>, GetRoomTypeByIdQueryValidator>();
        services.AddScoped<IRequestValidator<DeleteRoomTypeByIdCommand>, DeleteRoomTypeByIdCommandValidator>();
        services.AddScoped<IRequestValidator<GetRoomTypesInfoByPropertyIdQuery>, GetRoomTypesInfoByPropertyIdQueryValidator>();

        RoomTypeMappingConfiguration.AddEntityMapping();

        return services;
    }
}
