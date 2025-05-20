using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Properties;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PropertyRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Property>> GetAll( string city = "" )
    {
        return await _dbContext.Properties
            .ApplySearchByCityFilter( city )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Property> GetById( int propertyId )
    {
        return await _dbContext.Properties
            .FirstOrDefaultAsync( p => p.Id == propertyId );
    }

    public async Task Add( Property property )
    {
        await _dbContext.Properties.AddAsync( property );
    }

    public async Task Delete( Property property )
    {
        Property existingProperty = await GetById( property.Id );
        if ( existingProperty is not null )
        {
            _dbContext.Properties.Remove( property );
        }
    }

    public async Task Update( Property property )
    {
        Property existingProperty = await GetById( property.Id );
        if ( existingProperty is not null )
        {
            _dbContext.Properties.Update( property );
        }
    }
}
