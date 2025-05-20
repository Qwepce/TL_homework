using System.ComponentModel.DataAnnotations;
using WebAPI.Web.CustomAttributes;

namespace WebAPI.Web.Contracts.PropertyContracts;

public class UpdatePropertyContract
{
    [Required]
    [StringLength( 100, MinimumLength = 3, ErrorMessage = "{0} shoud be more than {2} and less than {1} characters" )]
    public string Name { get; init; }

    [Required]
    [StringLength( 100, MinimumLength = 3, ErrorMessage = "{0} shoud be more than {2} and less than {1} characters" )]
    public string Country { get; init; }

    [Required]
    [StringLength( 100, MinimumLength = 3, ErrorMessage = "{0} shoud be more than {2} and less than {1} characters" )]
    public string City { get; init; }

    [Required]
    [StringLength( 100, MinimumLength = 10, ErrorMessage = "{0} shoud be more than {2} and less than {1} characters" )]
    public string Address { get; init; }

    [Required]
    [DecimalRange( -90, 90 )]
    public decimal Latitude { get; init; }

    [Required]
    [DecimalRange( -180, 180 )]
    public decimal Longitude { get; init; }
}