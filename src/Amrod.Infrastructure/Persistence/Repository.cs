using System.Linq.Expressions;
using Amrod.Infrastructure.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Amrod.Infrastructure.Persistence;

public sealed class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public async Task Create(TEntity entity)
    {
         await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    
    public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync();
    }

    
    public IQueryable<TEntity> SearchEntity(SearchRequest request)
    {
        IQueryable<TEntity> query = _dbSet;
        
        var searchString = request.Search;
        var columns = request.Columns;
        var page = request.Page; 
        var pageSize = request.PageSize;
        var sortOrder = request.Sort;
        
        if(string.IsNullOrEmpty(searchString))
        {
            query = query.AsNoTracking();
            return query.Order().Skip(page).Take(pageSize).AsQueryable();
        }

        var filter = PredicateBuilderOrContains<TEntity>(searchString, columns);
        
        var totalCount = query.Count();
        
        var skip = (page - 1) * pageSize;
        
        query = query.Where(filter);
        
        query = query.Skip(skip).Take(pageSize);
        
        query = query.AsNoTracking();
        
        return query.AsQueryable();
    }
    
    private static Expression<Func<T, bool>> PredicateBuilderOrContains<T>(string searchString, string[] columns)
    {
        var parameter = Expression.Parameter(typeof(T));
        var predicate = PredicateBuilder.New<T>(false);
        foreach (var column in columns)
        {
            var property = Expression.Property(parameter, column);
            
            var entityColumnName = EF.Property<string>(property, column);
            
            //var caseInsensitiveExpression = EF.Functions.Collate(entityColumnName, "SQL_Latin1_General_CP1_CI_AS");
            
            //predicate = predicate.Or(e => EF.Functions.Like(caseInsensitiveExpression, $"{searchString}%"));
            //var likeExpression = EF.Functions.Like(, $"{searchString}%");
            
            //predicate = predicate.Or();
            
        }
        
        return predicate;
    }
    
    private static Expression<Func<T, object>> PredicateBuilderOrderBy<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var orderByExpression = Expression.Lambda<Func<T, object>>(property, parameter);
        return orderByExpression;
    }
}