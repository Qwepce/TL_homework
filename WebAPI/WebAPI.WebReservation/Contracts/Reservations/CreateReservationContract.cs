using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.WebReservation.Contracts.Reservations;

public class CreateReservationContract
{
    [Required]
    public int PropertyId { get; init; }

    [Required]
    public int RoomTypeId { get; init; }

    [Required]
    public DateOnly ArrivalDate { get; init; }

    [Required]
    public DateOnly DepartureDate { get; init; }

    [Required]
    [DisplayName( "Guest name" )]
    [StringLength( 150, MinimumLength = 5, ErrorMessage = "{0} must be more than {2} and less than {1} characters" )]
    public string GuestName { get; init; }

    [Required]
    [DisplayName( "Guest phone number" )]
    [StringLength( 20, MinimumLength = 10, ErrorMessage = "{0} must be more than {2} and less than {1} characters" )]
    public string GuestPhoneNumber { get; init; }

    [Required]
    public int GuestsCount { get; init; }

    [Required]
    [Length( minimumLength: 3, maximumLength: 3, ErrorMessage = "Currency must be in format XXX and length must be equals {0} characters" )]
    public string Currency { get; init; }
}
