using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IPropertyRepository :
    IAddEntityRepository<Property>,
    IDeleteEntityRepository<Property>,
    IUpdateEntityRepository<Property>,
    IGetEntityByIdRepository<Property>
{
    Task<IReadOnlyCollection<Property>> GetAll();

    Task<IReadOnlyCollection<Property>> GetAllByCity( string city );
}