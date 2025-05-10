using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Reservations.Commands.Create;
using WebAPI.Application.UseCases.Reservations.Commands.Delete;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAll;
using WebAPI.Application.UseCases.Reservations.Queries.GetById;
using WebAPI.Application.UseCases.Reservations.Queries.SearchAccommodations;
using WebAPI.WebReservation.Contracts.Reservations;
using WebAPI.WebReservation.Filters.Accommodations;
using WebAPI.WebReservation.Filters.Reservations;

namespace WebAPI.WebReservation.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReservationsController : ControllerBase
{
    [HttpGet( "search" )]
    public async Task<IActionResult> SearchAvailableAccommodations(
        [FromQuery] AccommodationsSearchFilter filters,
        [FromServices] IQueryHandler<SearchAccommodationsQuery, List<SearchResultDto>> queryHandler,
        CancellationToken cancellationToken )
    {
        SearchAccommodationsQuery query = filters.Adapt<SearchAccommodationsQuery>();
        Result<List<SearchResultDto>> result = await queryHandler.Handle( query, cancellationToken );

        return Ok( result.Value );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] ReservationsFilter filter,
        [FromServices] IQueryHandler<GetAllReservationsQuery, List<ReservationDto>> queryHandler,
        CancellationToken cancellationToken )
    {
        GetAllReservationsQuery query = filter.Adapt<GetAllReservationsQuery>();
        Result<List<ReservationDto>> result = await queryHandler.Handle( query, cancellationToken );

        List<ReadReservationContract> reservations = result.Value.Adapt<List<ReadReservationContract>>();

        return Ok( new { reservations } );
    }

    [HttpGet( "{reservationId:int}" )]
    public async Task<IActionResult> GetReservationById(
        [FromRoute] int reservationId,
        [FromServices] IQueryHandler<GetReservationByIdQuery, ReservationDto> queryHandler,
        CancellationToken cancellationToken )
    {
        GetReservationByIdQuery query = new() { ReservationId = reservationId };
        Result<ReservationDto> result = await queryHandler.Handle( query, cancellationToken );
        if ( result.IsFailure )
        {
            return NotFound( new { message = result.Errors } );
        }

        ReadReservationContract reservation = result.Value.Adapt<ReadReservationContract>();

        return Ok( new { reservation } );
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        [FromBody] CreateReservationContract request,
        [FromServices] ICommandHandlerWithResult<CreateReservationCommand, int> commandHandler,
        CancellationToken cancellationToken )
    {
        CreateReservationCommand command = request.Adapt<CreateReservationCommand>();
        Result<int> result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return CreatedAtAction(
            nameof( GetReservationById ),
            new { reservationId = result.Value },
            new { message = "Reservation was create successfully!" } );
    }

    [HttpDelete( "{reservationId:int}" )]
    public async Task<IActionResult> DeleteReservation(
        [FromRoute] int reservationId,
        [FromServices] ICommandHandler<DeleteReservationByIdCommand> commandHandler,
        CancellationToken cancellationToken )
    {
        DeleteReservationByIdCommand command = new() { ReservationId = reservationId };
        Result result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return NotFound( new { message = result.Errors } );
        }

        return Ok( new { message = "Reservation was delete successfully!" } );
    }
}
