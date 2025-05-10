using FluentValidation;
using FluentValidation.Results;
using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetById;
using WebAPI.Application.Utils;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.Reservations.Commands.Create;

public class CreateReservationCommandHandler : ICommandHandlerWithResult<CreateReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IValidator<CreateReservationCommand> _validator;
    private readonly IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> _roomTypeQueryHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceCalculator _calculator;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IValidator<CreateReservationCommand> validator,
        IUnitOfWork unitOfWork,
        IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> roomTypeQueryHandler,
        IPriceCalculator calculator )
    {
        _reservationRepository = reservationRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _roomTypeQueryHandler = roomTypeQueryHandler;
        _calculator = calculator;
    }

    public async Task<Result<int>> Handle( CreateReservationCommand command, CancellationToken cancellationToken )
    {
        ValidationResult validationResult = await _validator.ValidateAsync( command, cancellationToken );
        if ( !validationResult.IsValid )
        {
            List<Error> errors = validationResult.Errors
                .Select( e => new Error( e.ErrorMessage ) )
                .ToList();

            return Result<int>.Failure( errors );
        }

        GetRoomTypeByIdQuery getRoomTypeByIdQuery = new() { RoomTypeId = command.RoomTypeId };
        Result<RoomTypeDto> getRoomTypeByIdResult = await _roomTypeQueryHandler.Handle( getRoomTypeByIdQuery, cancellationToken );
        if ( getRoomTypeByIdResult.IsFailure )
        {
            return Result<int>.Failure( new Error( $"Room type with id {command.RoomTypeId} was not found" ) );
        }

        RoomTypeDto roomTypeDto = getRoomTypeByIdResult.Value;
        if ( roomTypeDto.PropertyId != command.PropertyId )
        {
            return Result<int>.Failure( new Error( $"Room type does not belong property" ) );
        }
        if ( roomTypeDto.MinPersonCount > command.GuestsCount || roomTypeDto.MaxPersonCount < command.GuestsCount )
        {
            return Result<int>.Failure( new Error( $"Guests count for selected room type msut be in range from {roomTypeDto.MinPersonCount} to {roomTypeDto.MaxPersonCount}" ) );
        }

        int overlappingReservations = await _reservationRepository.GetOverlappingReservationsCount(
            command.RoomTypeId,
            command.ArrivalDate,
            command.DepartureDate );
        if ( overlappingReservations >= roomTypeDto.TotalRoomsCount )
        {
            return Result<int>.Failure( new Error( "No available rooms for selected dates" ) );
        }

        Currency roomTypeCurrency = Enum.Parse<Currency>( roomTypeDto.Currency );
        Currency reservationCurrency = Enum.Parse<Currency>( command.ReservationCurrency, ignoreCase: true );
        int reservationDays = command.DepartureDate.DayNumber - command.ArrivalDate.DayNumber;
        decimal roomTypePrice = roomTypeDto.DailyPrice;
        decimal totalPrice = _calculator.CalculateTotalPrice(
            reservationDays,
            roomTypePrice,
            roomTypeCurrency,
            reservationCurrency );

        Reservation reservation = new(
            command.PropertyId,
            command.RoomTypeId,
            command.ArrivalDate,
            command.DepartureDate,
            command.GuestName.Trim(),
            command.GuestPhoneNumber.Trim(),
            reservationCurrency,
            totalPrice );

        await _reservationRepository.Create( reservation );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( reservation.Id );
    }
}
