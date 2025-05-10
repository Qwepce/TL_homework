using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IRoomTypeRepository :
    ICreateEntityRepository<RoomType>,
    IDeleteEntityRepository<RoomType>,
    IUpdateEntityRepository<RoomType>
{
    Task<IReadOnlyList<RoomType>> GetAllByPropertyId( int propertyId );

    Task<RoomType?> GetById( int roomTypeId );

    Task<List<RoomType>> GetByFilters( int propertyId, SearchRoomTypesFilter filter );
}
