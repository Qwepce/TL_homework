using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IRoomServiceRepository
{
    Task<IReadOnlyList<RoomService>> GetAllByNames( IEnumerable<string> names );

    Task AddRangeAsync( IEnumerable<RoomService> roomServices );
}
