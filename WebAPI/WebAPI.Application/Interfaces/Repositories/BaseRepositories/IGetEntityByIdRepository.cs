namespace WebAPI.Application.Interfaces.Repositories.BaseRepositories;

public interface IGetEntityByIdRepository<T> where T : class
{
    Task<T> GetById( int entityId );
}
