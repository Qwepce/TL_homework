namespace WebAPI.WebReservation.Filters.Accommodations;

public class AccommodationsSearchFilter
{
    public string City { get; init; } = string.Empty;

    public DateOnly? ArrivalDate { get; init; }

    public DateOnly? DepartureDate { get; init; }

    public int? GuestsNumber { get; init; }

    public decimal? MaxPrice { get; init; }

    public string Currency { get; init; } = string.Empty;
}