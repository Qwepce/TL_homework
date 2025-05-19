using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.DeletePropertyById;

public class DeletePropertyByIdCommandHandler : BaseCommandHandler<DeletePropertyByIdCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePropertyByIdCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<DeletePropertyByIdCommand> validator )
        : base( validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( DeletePropertyByIdCommand command, CancellationToken cancellationToken )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );

        await _propertyRepository.Delete( property );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}