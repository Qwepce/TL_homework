using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Commands.DeleteByIdCommand;

public class DeletePropertyByIdCommandValidator : IRequestValidator<DeletePropertyByIdCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IReservationRepository _reservationRepository;

    public DeletePropertyByIdCommandValidator( IPropertyRepository propertyRepository, IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<Result> Validate( DeletePropertyByIdCommand command )
    {
        Property property = await _propertyRepository.GetById( command.PropertyId );
        if ( property is null )
        {
            return Result.Failure( new Error( $"Property with ID: {command.PropertyId} was not found" ) );
        }

        bool isPropertyUsedInReservations = await _reservationRepository.IsPropertyUsedInReservations( command.PropertyId );
        if ( isPropertyUsedInReservations )
        {
            return Result.Failure( new Error( $"Property with ID: {command.PropertyId} used in existing reservations" ) );
        }

        return Result.Success();
    }
}
