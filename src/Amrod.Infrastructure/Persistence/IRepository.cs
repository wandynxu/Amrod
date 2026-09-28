using System.Linq.Expressions;

namespace Amrod.Infrastructure.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    
    void Create(TEntity entity);
    
    void Update(TEntity entity);
    
    //Include Related Entity
    IQueryable<TEntity> Get();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    IQueryable<TEntity> SearchEntity();
    
}