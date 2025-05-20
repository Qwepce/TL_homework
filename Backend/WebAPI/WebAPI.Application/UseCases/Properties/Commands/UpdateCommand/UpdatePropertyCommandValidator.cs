using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.BaseValidator;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.UpdateCommand;

public class UpdatePropertyCommandValidator : BasePropertyCommandsValidator<UpdatePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;

    public UpdatePropertyCommandValidator( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public override async Task<Result> Validate( UpdatePropertyCommand command )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {command.PropertyId} was not found" ) );
        }

        return await base.Validate( command );
    }
}
