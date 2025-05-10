namespace WebAPI.Application.Interfaces.Repositories.BaseRepositories;

public interface ICreateEntityRepository<T> where T : class
{
    Task Create( T entity );
}