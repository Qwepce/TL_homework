namespace WebAPI.Application.Interfaces.Repositories.BaseRepositories;

public interface IDeleteEntityRepository<T> where T : class
{
    Task Delete( T entity );
}