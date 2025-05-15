using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.RoomServices;

public class RoomServiceRepository : IRoomServiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomServiceRepository( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<RoomService>> GetAllByNames( IEnumerable<string> names )
    {
        return await _dbContext.RoomServices
            .Where( rs => names.Contains( rs.Name ) )
            .ToListAsync();
    }

    public async Task AddRangeAsync( IEnumerable<RoomService> roomServices )
    {
        await _dbContext.RoomServices.AddRangeAsync( roomServices );
    }
}
