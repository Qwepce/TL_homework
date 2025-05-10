using System.ComponentModel.DataAnnotations;
using WebAPI.Web.CustomAttributes;

namespace WebAPI.Web.Contracts.PropertyContracts;

public class UpdatePropertyContract
{
    [MaxLength( 100, ErrorMessage = "{0} must be less than {1} characters" )]
    [Required]
    public string Name { get; init; } = string.Empty;

    [MaxLength( 100, ErrorMessage = "{0} must be less than {1} characters" )]
    [Required]
    public string Country { get; init; } = string.Empty;

    [MaxLength( 100, ErrorMessage = "{0} must be less than {1} characters" )]
    [Required]
    public string City { get; init; } = string.Empty;

    [MaxLength( 100, ErrorMessage = "{0} must be less than {1} characters" )]
    [Required]
    public string Address { get; init; } = string.Empty;

    [DecimalRange( -90, 90 )]
    [Required]
    public decimal Latitude { get; init; }

    [DecimalRange( -180, 180 )]
    [Required]
    public decimal Longitude { get; init; }
}