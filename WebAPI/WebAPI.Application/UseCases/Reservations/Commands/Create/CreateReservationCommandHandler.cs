using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.UseCases.RoomTypes.Queries.GetById;
using WebAPI.Application.Utils;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.Reservations.Commands.Create;

public class CreateReservationCommandHandler : BaseCommandHandlerWithResult<CreateReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> _roomTypeQueryHandler;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IRequestValidator<CreateReservationCommand> validator,
        IUnitOfWork unitOfWork,
        IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> roomTypeQueryHandler )
        : base( validator )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _roomTypeQueryHandler = roomTypeQueryHandler;
    }

    protected override async Task<Result<int>> HandleCommand( CreateReservationCommand command, CancellationToken cancellationToken )
    {
        GetRoomTypeByIdQuery getRoomTypeByIdQuery = new() { RoomTypeId = command.RoomTypeId };
        Result<RoomTypeDto> getRoomTypeByIdResult = await _roomTypeQueryHandler.Handle( getRoomTypeByIdQuery, cancellationToken );
        RoomTypeDto roomTypeDto = getRoomTypeByIdResult.Value;

        Currency roomTypeCurrency = Enum.Parse<Currency>( roomTypeDto.Currency );
        Currency reservationCurrency = Enum.Parse<Currency>( command.ReservationCurrency, ignoreCase: true );
        int reservationDays = command.DepartureDate.DayNumber - command.ArrivalDate.DayNumber;
        decimal roomTypePrice = roomTypeDto.DailyPrice;
        decimal totalPrice = PriceCalculator.CalculateTotalPrice(
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

        await _reservationRepository.Add( reservation );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( reservation.Id );
    }
}
