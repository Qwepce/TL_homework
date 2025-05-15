using WebAPI.Application.Interfaces.Repositories;

namespace WebAPI.Infrastructure.Database.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork( ApplicationDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task CommitChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
