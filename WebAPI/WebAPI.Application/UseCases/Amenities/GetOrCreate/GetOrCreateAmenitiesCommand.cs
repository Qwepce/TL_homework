namespace WebAPI.Application.UseCases.Amenities.GetOrCreate;

public class GetOrCreateAmenitiesCommand
{
    public IEnumerable<string> AmenityNames { get; init; } = [];
}
