using System.ComponentModel.DataAnnotations;

namespace WebAPI.Web.Contracts.RoomTypeContracts;

public class UpdateRoomTypeContract
{
    [MaxLength( 30, ErrorMessage = "{0} must be less than {1} characters" )]
    [Required]
    public string Name { get; init; }

    [Required]
    public decimal DailyPrice { get; init; }

    [Length( minimumLength: 3, maximumLength: 3, ErrorMessage = "Currency must be in format XXX and length must be equals {0} characters" )]
    [Required]
    public string Currency { get; init; }

    [Required]
    public int MinPersonCount { get; init; }

    [Required]
    public int MaxPersonCount { get; init; }

    [Required]
    public int TotalRoomsCount { get; init; }

    [Required]
    public List<string> RoomServices { get; init; }

    [Required]
    public List<string> RoomAmenities { get; init; }
}