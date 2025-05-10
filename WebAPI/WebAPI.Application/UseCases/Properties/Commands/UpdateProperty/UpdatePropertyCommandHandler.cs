using FluentValidation;
using FluentValidation.Results;
using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommandHandler : ICommandHandler<UpdatePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePropertyCommand> _validator;

    public UpdatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdatePropertyCommand> validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle( UpdatePropertyCommand command, CancellationToken cancellationToken )
    {
        ValidationResult validationResult = await _validator.ValidateAsync( command, cancellationToken );
        if ( !validationResult.IsValid )
        {
            List<Error> errors = validationResult.Errors
                .Select( error => new Error( error.ErrorMessage ) )
                .ToList();

            return Result.Failure( errors );
        }

        Property? existingProperty = await _propertyRepository.GetById( command.PropertyId );
        if ( existingProperty is null )
        {
            return Result.Failure( new Error( $"Property with id {command.PropertyId} was not found" ) );
        }

        command.Adapt( existingProperty );

        await _propertyRepository.Update( existingProperty );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}
