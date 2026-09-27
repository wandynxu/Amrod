using Amrod.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Amrod.Infrastructure;

public sealed class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public IQueryable<TEntity> Search()
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetById(int id)
    {
        var result = await _dbSet.FindAsync(id);
        return result ??  throw new Exception();
    }

    public void Add(TEntity entity)
    {
        _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    
}