using WebAPI.Domain.Models.Enums;

namespace WebAPI.Domain.Models.Entities;

public class Reservation : IEntityId<int>
{
    public int Id { get; init; }

    public int PropertyId { get; init; }

    public int RoomTypeId { get; init; }

    public DateOnly ArrivalDate { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DateOnly DepartureDate { get; set; }

    public DateTime DepartureTime { get; set; }

    public string GuestName { get; set; }

    public string GuestPhoneNumber { get; set; }

    public Currency ReservationCurrency { get; set; }

    public decimal TotalPrice { get; set; }

    public void SetBaseArrivalAndDepartureTimes()
    {
        ArrivalTime = ArrivalDate.ToDateTime( new TimeOnly( 12, 0 ) );
        DepartureTime = DepartureDate.ToDateTime( new TimeOnly( 12, 0 ) );
    }

    public Reservation(
        int propertyId,
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        string guestName,
        string guestPhoneNumber,
        Currency reservationCurrency,
        decimal totalPrice )
    {
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        ReservationCurrency = reservationCurrency;
        TotalPrice = totalPrice;

        SetBaseArrivalAndDepartureTimes();
    }
}