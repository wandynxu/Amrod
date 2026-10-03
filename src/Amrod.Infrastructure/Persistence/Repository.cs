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
        
        var columns = request.Columns;
        var page = request.Page; 
        var pageSize = request.PageSize;
        var sortOrder = request.Sort;
        
        if(!request.SearchTerms.Any())
        {
            query = query.AsNoTracking();
            return query.Order().Skip(0).Take(pageSize).AsExpandable();
        }

        var filter = PredicateBuilderOrContains<TEntity>(request.SearchTerms, columns);
        
        var totalCount = query.Count();
        
        var skip = (page - 1) * pageSize;
        
        query = query.Where(filter);
        
        query = query.Skip(skip).Take(pageSize);
        
        query = query.AsNoTracking();
        
        return query.AsExpandable();
    }
    
    private static Expression<Func<T, bool>> PredicateBuilderOrContains<T>(string[] searchTerms, string[] columns)
    {
        var parameter = Expression.Parameter(typeof(T));
        
        var predicate = PredicateBuilder.New<T>(true);
        var efFunctionsInstance = Expression.Constant(EF.Functions);
        
        var likeMethod = typeof(DbFunctionsExtensions).GetMethod(
            nameof(DbFunctionsExtensions.Like),
            [typeof(DbFunctions), typeof(string), typeof(string)]
        );
        
        if (likeMethod is not null)
        {
            foreach (var searchString in searchTerms)
            {
                var orPredicate = PredicateBuilder.New<T>(false);
                foreach (var column in columns)
                {
                    var exprCall = Expression.Call(null,likeMethod, efFunctionsInstance, Expression.Property(parameter, column), Expression.Constant($"%{searchString}%"));
                    
                    var lambda = Expression.Lambda<Func<T, bool>>(exprCall, parameter);
                    orPredicate = orPredicate.Or(lambda);
                }
                
                predicate  = predicate.And(orPredicate);
                
            }    
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