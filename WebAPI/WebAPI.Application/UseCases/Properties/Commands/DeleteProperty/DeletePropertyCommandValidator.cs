using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandValidator : IRequestValidator<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;

    public DeletePropertyCommandValidator( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result> Validate( DeletePropertyCommand command )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {command.PropertyId} was not found" ) );
        }

        return Result.Success();
    }
}
