using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.UseCases.Properties.Commands.CreateProperty;
using WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;
using WebAPI.Application.UseCases.Reservations.Commands.Create;
using WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;
using WebAPI.Application.Validation.Properties;
using WebAPI.Application.Validation.Reservations;
using WebAPI.Application.Validation.RoomTypes;

namespace WebAPI.Application.Validation;

public static class ValidationBindings
{
    public static IServiceCollection AddValidationBindings( this IServiceCollection services )
    {
        services.AddScoped<IValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
        services.AddScoped<IValidator<UpdatePropertyCommand>, UpdatePropertyCommandValidator>();
        services.AddScoped<IValidator<CreateRoomTypeCommand>, CreateRoomTypeCommandValidator>();
        services.AddScoped<IValidator<CreateReservationCommand>, CreateReservationCommandValidator>();
        services.AddScoped<IValidator<UpdateRoomTypeCommand>, UpdateRoomTypeCommandValidator>();

        return services;
    }
}
