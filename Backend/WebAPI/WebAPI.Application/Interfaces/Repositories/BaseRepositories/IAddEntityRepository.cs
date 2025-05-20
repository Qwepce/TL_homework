namespace WebAPI.Application.Interfaces.Repositories.BaseRepositories;

public interface IAddEntityRepository<T> where T : class
{
    Task Add( T entity );
}