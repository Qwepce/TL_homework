using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.Reservations.Commands.CreateCommand;
using WebAPI.Application.UseCases.Reservations.Commands.DeleteByIdCommand;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAllQuery;
using WebAPI.Application.UseCases.Reservations.Queries.GetByIdQuery;
using WebAPI.Application.UseCases.Reservations.Queries.SearchAvailableReservations;

namespace WebAPI.Application.UseCases.Reservations;

public static class ReservationBindings
{
    public static IServiceCollection AddReservationBindings( this IServiceCollection services )
    {
        services.AddScoped<IQueryHandler<GetAllReservationsQuery, List<ReservationDto>>, GetAllReservationsQueryHandler>();
        services.AddScoped<IQueryHandler<GetReservationByIdQuery, ReservationDto>, GetReservationByIdQueryHandler>();
        services.AddScoped<IQueryHandler<SearchAvailableReservationsQuery, List<SearchResultDto>>, SearchAvailableReservationsQueryHandler>();

        services.AddScoped<ICommandHandler<DeleteReservationByIdCommand>, DeleteReservationByIdCommandHandler>();
        services.AddScoped<ICommandHandlerWithResult<CreateReservationCommand, int>, CreateReservationCommandHandler>();

        services.AddScoped<IRequestValidator<GetReservationByIdQuery>, GetReservationByIdQueryValidator>();
        services.AddScoped<IRequestValidator<DeleteReservationByIdCommand>, DeleteReservationByIdCommandValidator>();
        services.AddScoped<IRequestValidator<CreateReservationCommand>, CreateReservationCommandValidator>();

        ReservationMappingConfiguration.AddEntityMapping();

        return services;
    }
}