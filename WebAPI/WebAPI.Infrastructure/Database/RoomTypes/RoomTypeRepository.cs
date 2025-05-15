using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Extensions;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.RoomTypes;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomTypeRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<RoomType>> GetAllRoomTypesInfoByPropertyId( int propertyId )
    {
        return await _dbContext.RoomTypes
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.Amenities )
            .Where( rt => rt.PropertyId == propertyId )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<RoomType>> GetByFilters( SearchRoomTypesFilter filter )
    {
        return await _dbContext.RoomTypes
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.Amenities )
            .ApplySearchFilters( filter )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RoomType> GetById( int roomTypeId )
    {
        return await _dbContext.RoomTypes
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.Amenities )
            .FirstOrDefaultAsync( rt => rt.Id == roomTypeId );
    }

    public async Task Add( RoomType roomType )
    {
        await _dbContext.RoomTypes.AddAsync( roomType );
    }

    public async Task Delete( RoomType roomType )
    {
        RoomType existingRoomType = await GetById( roomType.Id );
        if ( existingRoomType is not null )
        {
            _dbContext.Remove( existingRoomType );
        }
    }

    public async Task Update( RoomType roomType )
    {
        RoomType existedRoomType = await GetById( roomType.Id );
        if ( existedRoomType is not null )
        {
            _dbContext.RoomTypes.Update( existedRoomType );
        }
    }
}
