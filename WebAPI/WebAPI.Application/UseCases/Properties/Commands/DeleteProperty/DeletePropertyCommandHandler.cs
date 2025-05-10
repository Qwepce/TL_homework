using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandHandler : ICommandHandler<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePropertyCommandHandler( IPropertyRepository propertyRepository, IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle( DeletePropertyCommand command, CancellationToken cancellationToken )
    {
        Property? existingProperty = await _propertyRepository.GetById( command.PropertyId );

        if ( existingProperty is null )
        {
            return Result.Failure( new Error( $"Property with id {command.PropertyId} was not found!" ) );
        }

        try
        {
            await _propertyRepository.Delete( existingProperty );
            await _unitOfWork.CommitChangesAsync();
        }
        catch ( Exception )
        {
            return Result.Failure( new Error( "Error while trying to delete property. Perhaps it's belong to unclosed reservation." ) );
        }

        return Result.Success();
    }
}