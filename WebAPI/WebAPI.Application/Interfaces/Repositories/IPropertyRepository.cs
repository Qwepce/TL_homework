using WebAPI.Application.Interfaces.Repositories.BaseRepositories;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.Interfaces.Repositories;

public interface IPropertyRepository :
    ICreateEntityRepository<Property>,
    IDeleteEntityRepository<Property>,
    IUpdateEntityRepository<Property>
{
    Task<IReadOnlyCollection<Property>> GetAll();

    Task<Property?> GetById( int propertyId );

    Task<IReadOnlyList<Property>> GetAllByCity( string city );
}