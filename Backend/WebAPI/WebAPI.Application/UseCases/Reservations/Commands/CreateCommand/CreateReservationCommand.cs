namespace WebAPI.Application.UseCases.Reservations.Commands.CreateCommand;

public class CreateReservationCommand
{
    public int PropertyId { get; init; }

    public int RoomTypeId { get; init; }

    public DateOnly ArrivalDate { get; init; }

    public DateOnly DepartureDate { get; init; }

    public string GuestName { get; init; }

    public string GuestPhoneNumber { get; init; }

    public int GuestsCount { get; set; }

    public string ReservationCurrency { get; init; }
}