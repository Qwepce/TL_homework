using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IAmenityRepository
{
    Task<IReadOnlyList<Amenity>> GetAllByNames( IEnumerable<string> names );

    Task AddRangeAsync( IEnumerable<Amenity> roomAmenities );
}
