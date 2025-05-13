using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommandHandler : BaseCommandHandler<UpdatePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<UpdatePropertyCommand> validator ) : base( validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( UpdatePropertyCommand command, CancellationToken cancellationToken )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );

        command.Adapt( property );

        await _propertyRepository.Update( property );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}
