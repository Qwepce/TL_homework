using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.Reservations.Commands.Create;
using WebAPI.Application.UseCases.Reservations.Commands.Delete;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAll;
using WebAPI.Application.UseCases.Reservations.Queries.GetById;
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