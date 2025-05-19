using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.Commands.CreateProperty;
using WebAPI.Application.UseCases.Properties.Commands.DeletePropertyById;
using WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Application.UseCases.Properties.Queries.GetAllProperties;
using WebAPI.Application.UseCases.Properties.Queries.GetPropertyById;
using WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetRoomTypesInfoByPropertyId;
using WebAPI.Web.Contracts.PropertyContracts;
using WebAPI.Web.Contracts.RoomTypeContracts;

namespace WebAPI.Web.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class PropertiesController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllProperties(
        [FromServices] IQueryHandler<GetAllPropertiesQuery, List<PropertyDto>> queryHandler,
        CancellationToken cancellationToken )
    {
        GetAllPropertiesQuery query = new();
        Result<List<PropertyDto>> result = await queryHandler.Handle( query, cancellationToken );

        List<ReadPropertyContract> properties = result.Value.Adapt<List<ReadPropertyContract>>();

        return Ok( new { properties } );
    }

    [HttpGet( "{propertyId:int}" )]
    public async Task<IActionResult> GetPropertyById(
        [FromRoute] int propertyId,
        [FromServices] IQueryHandler<GetPropertyByIdQuery, PropertyDto> queryHandler,
        CancellationToken cancellationToken )
    {
        GetPropertyByIdQuery query = new()
        {
            PropertyId = propertyId
        };
        Result<PropertyDto> result = await queryHandler.Handle( query, cancellationToken );

        if ( result.IsFailure )
        {
            return NotFound( new { result.Errors } );
        }

        ReadPropertyContract property = result.Value.Adapt<ReadPropertyContract>();

        return Ok( new { property } );
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty(
        [FromBody] CreatePropertyContract request,
        [FromServices] ICommandHandlerWithResult<CreatePropertyCommand, int> commandHandler,
        CancellationToken cancellationToken )
    {
        CreatePropertyCommand createCommand = request.Adapt<CreatePropertyCommand>();
        Result<int> result = await commandHandler.Handle( createCommand, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return CreatedAtAction(
            nameof( GetPropertyById ),
            new { propertyId = result.Value },
            new { } );
    }

    [HttpPut( "{propertyId:int}" )]
    public async Task<IActionResult> UpdateProperty(
        [FromRoute] int propertyId,
        [FromBody] UpdatePropertyContract request,
        [FromServices] ICommandHandler<UpdatePropertyCommand> commandHandler,
        CancellationToken cancellationToken )
    {
        UpdatePropertyCommand command = request.Adapt<UpdatePropertyCommand>();
        command.PropertyId = propertyId;
        Result result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return Ok();
    }

    [HttpDelete( "{propertyId:int}" )]
    public async Task<IActionResult> DeleteProperty(
        [FromRoute] int propertyId,
        [FromServices] ICommandHandler<DeletePropertyByIdCommand> commandHandler,
        CancellationToken cancellationToken )
    {
        DeletePropertyByIdCommand command = new()
        {
            PropertyId = propertyId
        };
        Result result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return NotFound( new { message = result.Errors } );
        }

        return NoContent();
    }

    [HttpGet( "{propertyId:int}/roomtypes" )]
    public async Task<IActionResult> GetRoomTypesByPropertyId(
        [FromRoute] int propertyId,
        [FromServices] IQueryHandler<GetRoomTypesInfoByPropertyIdQuery, IReadOnlyList<RoomTypeDto>> queryHandler,
        CancellationToken cancellationToken )
    {
        GetRoomTypesInfoByPropertyIdQuery query = new()
        {
            PropertyId = propertyId
        };
        Result<IReadOnlyList<RoomTypeDto>> result = await queryHandler.Handle( query, cancellationToken );

        if ( result.IsFailure )
        {
            return NotFound( new { errors = result.Errors } );
        }

        IReadOnlyList<ReadRoomTypeContract> roomTypes = result.Value.Adapt<IReadOnlyList<ReadRoomTypeContract>>();

        return Ok( roomTypes );
    }

    [HttpPost( "{propertyId:int}/roomtypes" )]
    public async Task<IActionResult> AddNewRoomTypeToProperty(
        [FromRoute] int propertyId,
        [FromBody] CreateRoomTypeContract request,
        [FromServices] ICommandHandlerWithResult<CreateRoomTypeCommand, int> commandHandler,
        CancellationToken cancellationToken )
    {
        CreateRoomTypeCommand command = request.Adapt<CreateRoomTypeCommand>();
        command.PropertyId = propertyId;

        Result<int> result = await commandHandler.Handle( command, cancellationToken );

        if ( result.IsFailure )
        {
            return BadRequest( new { errors = result.Errors } );
        }

        return CreatedAtAction(
            nameof( RoomTypesController.GetRoomTypeById ),
            controllerName: "RoomTypes",
            new { roomTypeId = result.Value },
            new { }
        );
    }
}
