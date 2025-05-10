using FluentValidation;
using FluentValidation.Results;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : ICommandHandlerWithResult<CreatePropertyCommand, int>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePropertyCommand> _validator;

    public CreatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreatePropertyCommand> validator )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<int>> Handle( CreatePropertyCommand command, CancellationToken cancellationToken )
    {
        ValidationResult validationResult = await _validator.ValidateAsync( command, cancellationToken );
        if ( !validationResult.IsValid )
        {
            List<Error> errors = validationResult.Errors
                .Select( error => new Error( error.ErrorMessage ) )
                .ToList();

            return Result<int>.Failure( errors );
        }

        Property property = new(
            command.Name.Trim(),
            command.Country.Trim(),
            command.City.Trim(),
            command.Address.Trim(),
            command.Latitude,
            command.Longitude );

        await _propertyRepository.Create( property );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( property.Id );
    }
}