using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Commands.DeleteByIdCommand;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateCommand;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetByIdQuery;
using WebAPI.Web.Contracts.RoomTypeContracts;

namespace WebAPI.Web.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class RoomTypesController : ControllerBase
{

    [HttpGet( "{roomTypeId:int}" )]
    public async Task<IActionResult> GetRoomTypeById(
        [FromRoute] int roomTypeId,
        [FromServices] IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> queryHandler,
        CancellationToken cancellationToken )
    {
        GetRoomTypeByIdQuery query = new() { RoomTypeId = roomTypeId };
        Result<RoomTypeDto> result = await queryHandler.Handle( query, cancellationToken );

        if ( result.IsFailure )
        {
            return NotFound( new { errors = result.Errors } );
        }

        ReadRoomTypeContract roomType = result.Value.Adapt<ReadRoomTypeContract>();

        return Ok( new { roomType } );
    }

    [HttpPut( "{roomTypeId:int}" )]
    public async Task<IActionResult> UpdateRoomType(
        [FromRoute] int roomTypeId,
        [FromBody] UpdateRoomTypeContract request,
        [FromServices] ICommandHandler<UpdateRoomTypeCommand> commandHandler,
        CancellationToken cancellationToken )
    {
        UpdateRoomTypeCommand command = request.Adapt<UpdateRoomTypeCommand>();
        command.RoomTypeId = roomTypeId;
        Result result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return Ok();
    }

    [HttpDelete( "{roomTypeId:int}" )]
    public async Task<IActionResult> DeleteRoomType(
        [FromRoute] int roomTypeId,
        [FromServices] ICommandHandler<DeleteRoomTypeByIdCommand> commandHandler,
        CancellationToken cancellationToken )
    {
        DeleteRoomTypeByIdCommand command = new()
        {
            RoomTypeId = roomTypeId
        };
        Result result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return Ok();
    }
}
