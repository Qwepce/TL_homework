using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PropertyRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Property>> GetAll()
    {
        return await _dbContext.Properties
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Property?> GetById( int propertyId )
    {
        return await _dbContext.Properties
            .FirstOrDefaultAsync( p => p.Id == propertyId );
    }

    public async Task<IReadOnlyList<Property>> GetAllByCity( string? city )
    {
        return await _dbContext.Properties
            .WhereIf( !string.IsNullOrWhiteSpace( city ), p => p.City.ToLower().Equals( city.ToLower() ) )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Create( Property entity )
    {
        await _dbContext.Properties.AddAsync( entity );
    }

    public async Task Delete( Property entity )
    {
        await _dbContext.Properties
            .Where( p => p.Id == entity.Id )
            .ExecuteDeleteAsync();
    }

    public async Task Update( Property entity )
    {
        Property? existingProperty = await GetById( entity.Id );

        if ( existingProperty is not null )
        {
            _dbContext.Properties.Update( entity );
        }
    }
}
