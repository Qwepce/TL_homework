using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IRoomTypeRepository :
    IAddEntityRepository<RoomType>,
    IDeleteEntityRepository<RoomType>,
    IUpdateEntityRepository<RoomType>,
    IGetEntityByIdRepository<RoomType>
{
    Task<IReadOnlyList<RoomType>> GetAllRoomTypesInfoByPropertyId( int propertyId );

    Task<List<RoomType>> GetByFilters( SearchRoomTypesFilter filter );
}
