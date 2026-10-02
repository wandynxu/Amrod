using System.Linq.Expressions;

namespace Amrod.Infrastructure.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    
    void Create(TEntity entity);
    
    void Update(TEntity entity);
    Task Delete(Guid id);
    
    Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null);
    IQueryable<TEntity> SearchEntity();
    
}