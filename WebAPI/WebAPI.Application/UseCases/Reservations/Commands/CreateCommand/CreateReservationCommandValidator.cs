using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.Reservations.Commands.CreateCommand;

public class CreateReservationCommandValidator : IRequestValidator<CreateReservationCommand>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    private readonly List<Error> _errors = [];

    public CreateReservationCommandValidator(
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _reservationRepository = reservationRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result> Validate( CreateReservationCommand command )
    {
        await ApplyRoomTypeCheck(
            command.RoomTypeId,
            command.PropertyId,
            command.GuestsCount,
            command.ArrivalDate,
            command.DepartureDate );

        ApplyDatesCheck( command.ArrivalDate, command.DepartureDate );

        ApplyStringsCheck( new()
        {
            {"Guest name", (command.GuestName, MaxLength: 100) },
            {"Guest phone number", (command.GuestPhoneNumber, MaxLength: 20) }
        } );

        if ( command.GuestsCount <= 0 )
        {
            _errors.Add( new Error( "Guests count cannot be lower than 1" ) );
        }

        if ( !Enum.TryParse( typeof( Currency ), command.ReservationCurrency, ignoreCase: true, out object _ ) )
        {
            _errors.Add( new Error( $"Invalid currency" ) );
        }

        if ( _errors.Count != 0 )
        {
            return Result.Failure( _errors );
        }

        return Result.Success();
    }

    private void ApplyDatesCheck( DateOnly arrivalDate, DateOnly departureDate )
    {
        if ( arrivalDate <= DateOnly.FromDateTime( DateTimeOffset.UtcNow.DateTime ) )
        {
            _errors.Add( new Error( "Arrival date must be in the future" ) );
        }

        if ( departureDate <= arrivalDate )
        {
            _errors.Add( new Error( "Departure date must be after arrival date" ) );
        }
    }

    private void ApplyStringsCheck( Dictionary<string, (string FieldValue, int MaxLength)> checks )
    {
        foreach ( KeyValuePair<string, (string FieldValue, int MaxLength)> kvp in checks )
        {
            if ( string.IsNullOrWhiteSpace( kvp.Value.FieldValue ) )
            {
                _errors.Add( new Error( $"{kvp.Key} is required" ) );
                continue;
            }
            if ( kvp.Value.FieldValue.Length > kvp.Value.MaxLength )
            {
                _errors.Add( new Error( $"{kvp.Key} must be less or equal than {kvp.Value.MaxLength} characters" ) );
            }
        }
    }

    private async Task ApplyRoomTypeCheck(
        int roomTypeId,
        int propertyId,
        int guestsCount,
        DateOnly arrivalDate,
        DateOnly departureDate )
    {
        RoomType roomType = await _roomTypeRepository.GetById( roomTypeId );
        if ( roomType is null )
        {
            _errors.Add( new Error( $"Room type with id {roomTypeId} was not found" ) );
            return;
        }

        if ( roomType.PropertyId != propertyId )
        {
            _errors.Add( new Error( $"Room type does not belong property" ) );
        }
        if ( roomType.MinPersonCount > guestsCount || roomType.MaxPersonCount < guestsCount )
        {
            _errors.Add( new Error( $"Guests count for selected room type must be in range from {roomType.MinPersonCount} to {roomType.MaxPersonCount}" ) );
        }

        int overlappingReservations = await _reservationRepository.GetReservationsCountByCategoryAndDates(
            roomTypeId,
            arrivalDate,
            departureDate );
        if ( overlappingReservations == roomType.TotalRoomsCount )
        {
            _errors.Add( new Error( "No available rooms for selected dates" ) );
        }
    }
}
