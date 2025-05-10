using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.MappingConfigurations.Reservations;
using WebAPI.Application.UseCases.Reservations.Commands.Create;
using WebAPI.Application.UseCases.Reservations.Commands.Delete;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAll;
using WebAPI.Application.UseCases.Reservations.Queries.GetById;
using WebAPI.Application.UseCases.Reservations.Queries.SearchAccommodations;

namespace WebAPI.Application.Bindings.Reservations;

public static class ReservationBindings
{
    public static IServiceCollection AddReservationBindings( this IServiceCollection services )
    {
        services.AddScoped<IQueryHandler<GetAllReservationsQuery, List<ReservationDto>>, GetAllReservationsQueryHandler>();
        services.AddScoped<IQueryHandler<GetReservationByIdQuery, ReservationDto>, GetReservationByIdQueryHandler>();
        services.AddScoped<IQueryHandler<SearchAccommodationsQuery, List<SearchResultDto>>, SearchAccommodationsQueryHandler>();
        services.AddScoped<ICommandHandler<DeleteReservationByIdCommand>, DeleteReservationByIdCommandHandler>();
        services.AddScoped<ICommandHandlerWithResult<CreateReservationCommand, int>, CreateReservationCommandHandler>();

        ReservationMappingConfiguration.AddEntityMapping();

        return services;
    }
}
