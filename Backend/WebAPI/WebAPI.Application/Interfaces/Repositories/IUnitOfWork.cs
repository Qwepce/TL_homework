namespace WebAPI.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
