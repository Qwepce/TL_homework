using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomTypeRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task Create( RoomType entity )
    {
        await _dbContext.RoomTypes.AddAsync( entity );
    }

    public async Task Delete( RoomType entity )
    {
        RoomType? existingRoomType = await GetById( entity.Id );

        if ( existingRoomType is not null )
        {
            _dbContext.Remove( existingRoomType );
        }
    }

    public async Task<IReadOnlyList<RoomType>> GetAllByPropertyId( int propertyId )
    {
        return await _dbContext.RoomTypes
            .Include( rt => rt.Services )
            .Include( rt => rt.Amenities )
            .Where( rt => rt.PropertyId == propertyId )
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<RoomType>> GetByFilters( int propertyId, SearchRoomTypesFilter filter )
    {
        return _dbContext.RoomTypes
            .Include( rt => rt.Services )
            .Include( rt => rt.Amenities )
            .Where( rt => rt.PropertyId == propertyId )
            .WhereIf( filter.GuestsNumber.HasValue, rt => rt.MinPersonCount <= filter.GuestsNumber && rt.MaxPersonCount >= filter.GuestsNumber )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RoomType?> GetById( int roomTypeId )
    {
        return await _dbContext.RoomTypes
            .Include( rt => rt.Services )
            .Include( rt => rt.Amenities )
            .FirstOrDefaultAsync( rt => rt.Id == roomTypeId );
    }

    public async Task Update( RoomType entity )
    {
        RoomType? existedRoomType = await GetById( entity.Id );

        if ( existedRoomType is not null )
        {
            _dbContext.RoomTypes.Update( existedRoomType );
        }
    }
}
