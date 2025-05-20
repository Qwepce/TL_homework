using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.BaseValidator;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.CreateCommand;

public class CreateRoomTypeCommandValidator : BaseRoomTypeCommandsValidator<CreateRoomTypeCommand>
{
    private readonly IPropertyRepository _propertyRepository;

    public CreateRoomTypeCommandValidator( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public override async Task<Result> Validate( CreateRoomTypeCommand command )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {command.PropertyId} was not found" ) );
        }

        return await base.Validate( command );
    }
}
