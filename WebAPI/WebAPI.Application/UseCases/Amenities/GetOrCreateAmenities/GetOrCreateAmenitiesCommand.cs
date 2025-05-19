namespace WebAPI.Application.UseCases.Amenities.GetOrCreateAmenities;

public class GetOrCreateAmenitiesCommand
{
    public IEnumerable<string> AmenityNames { get; init; } = [];
}
