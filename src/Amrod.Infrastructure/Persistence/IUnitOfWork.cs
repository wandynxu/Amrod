namespace Amrod.Infrastructure.Persistence;

public interface IUnitOfWork  : IDisposable
{
    Task CommitAsync();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}