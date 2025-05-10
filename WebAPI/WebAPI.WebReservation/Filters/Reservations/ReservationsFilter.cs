namespace WebAPI.WebReservation.Filters.Reservations;

public class ReservationsFilter
{
    public int? PropertyId { get; init; }

    public int? RoomTypeId { get; init; }

    public DateOnly? ArrivalDate { get; init; }

    public DateOnly? DepartureDate { get; init; }

    public string GuestName { get; init; } = string.Empty;

    public string GuestPhoneNumber { get; init; } = string.Empty;

    public string ReservationCurrency { get; init; } = string.Empty;
}
