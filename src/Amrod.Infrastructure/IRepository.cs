namespace Amrod.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Search();
    
    Task<TEntity> GetById(int id);
    
    void Add(TEntity entity);
    
    void Update(TEntity entity);
    
}