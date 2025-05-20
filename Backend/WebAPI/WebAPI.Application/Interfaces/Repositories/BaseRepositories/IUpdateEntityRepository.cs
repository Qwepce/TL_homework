namespace WebAPI.Application.Interfaces.Repositories.BaseRepositories;

public interface IUpdateEntityRepository<T> where T : class
{
    Task Update( T entity );
}