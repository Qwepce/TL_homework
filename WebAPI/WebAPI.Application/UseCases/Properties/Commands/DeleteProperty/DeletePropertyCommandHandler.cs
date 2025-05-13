using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandHandler : BaseCommandHandler<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<DeletePropertyCommand> validator ) : base( validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( DeletePropertyCommand command, CancellationToken cancellationToken )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );

        try
        {
            await _propertyRepository.Delete( property );
            await _unitOfWork.CommitChangesAsync();
        }
        catch ( Exception )
        {
            return Result.Failure( new Error( "Error while trying to delete property. Perhaps it's belong to unclosed reservation." ) );
        }

        return Result.Success();
    }
}