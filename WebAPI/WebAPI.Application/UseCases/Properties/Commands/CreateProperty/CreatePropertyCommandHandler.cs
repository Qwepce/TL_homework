using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : BaseCommandHandlerWithResult<CreatePropertyCommand, int>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<CreatePropertyCommand> validator )
        : base( validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result<int>> HandleCommand( CreatePropertyCommand command, CancellationToken cancellationToken )
    {
        Property property = new(
            command.Name.Trim(),
            command.Country.Trim(),
            command.City.Trim(),
            command.Address.Trim(),
            command.Latitude,
            command.Longitude );

        await _propertyRepository.Add( property );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( property.Id );
    }
}