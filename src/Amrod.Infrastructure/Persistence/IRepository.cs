using System.Linq.Expressions;
using Amrod.Infrastructure.Models;

namespace Amrod.Infrastructure.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    
    Task Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    
    Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null);
    IQueryable<TEntity> SearchEntity(SearchRequest request);
    
}