namespace WebAPI.Application.UseCases.Reservations.Dto;

public class ReservationDto
{
    public int PropertyId { get; init; }

    public int RoomTypeId { get; init; }

    public DateOnly ArrivalDate { get; init; }

    public DateOnly DepartureDate { get; init; }

    public string GuestName { get; init; }

    public string GuestPhoneNumber { get; init; }

    public decimal TotalPrice { get; init; }

    public string ReservationCurrency { get; init; }
}