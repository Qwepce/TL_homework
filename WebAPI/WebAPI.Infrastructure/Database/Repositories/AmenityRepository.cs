using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Repositories;

public class AmenityRepository : IAmenityRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AmenityRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Amenity>> GetAllByNames( IEnumerable<string> names )
    {
        return await _dbContext.Amenities
            .Where( a => names.Contains( a.Name ) )
            .ToListAsync();
    }

    public async Task CreateRangeAsync( IEnumerable<Amenity> roomAmenities )
    {
        await _dbContext.Amenities.AddRangeAsync( roomAmenities );
    }
}
