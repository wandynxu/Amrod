using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Amrod.Infrastructure.Persistence;

public sealed class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public void Create(TEntity entity)
    {
          _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        //_dbSet.Attach(entity);
        //_dbSet.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
        
    }

    public async Task Delete(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);    
        }
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

    
    
    public IQueryable<TEntity> SearchEntity()
    {
        IQueryable<TEntity> query = _dbSet;
        //var searchString = request.SearchString;
        //var columns = request.Columns;
        //columns = columns.Select(c => $"{char.ToUpper(c[0])}{c[1..]}").ToArray();

        //if (string.IsNullOrWhiteSpace(searchString) && columns.Length == 0) return Get(new Request { PageNumber = request.PageNumber, PageSize = request.PageSize }); 
        
        //var filter = PredicateBuilderOrContains<TEntity>(searchString, columns);
        return query.Take(10).AsQueryable();
    }
    
    private static Expression<Func<T, bool>> PredicateBuilderOrContains<T>(string searchString, string[] columns)
    {
        var parameter = Expression.Parameter(typeof(T));
        var searchText = Expression.Constant($"%{searchString}%");
        var predicate = PredicateBuilder.New<T>();
        
        /*
        var entity = typeof(T);
        var properties = entity.GetProperties();
        foreach (var column in columns)
        {
            var columnType = properties?.FirstOrDefault(p => p.Name == column)?.PropertyType.Name;
                
            var property = Expression.Property(parameter, column);
            
            var efFunctions = Expression.Property(null, typeof(EF), nameof(EF.Functions));
            
            var iLikeMethod = typeof(NpgsqlDbFunctionsExtensions).GetMethod(nameof(NpgsqlDbFunctionsExtensions.ILike),
                [typeof(DbFunctions), typeof(string), typeof(string)]);
            
            MethodCallExpression exprCall;
            if (columnType is not null && !columnType.Equals("String"))
            {
                var convertProperty = Expression.Call(property, typeof(object).GetMethod(nameof(ToString), Type.EmptyTypes)!);
                exprCall = Expression.Call(iLikeMethod!, efFunctions, convertProperty, searchText);
            }
            else
            {
                exprCall = Expression.Call(iLikeMethod!, efFunctions, property, searchText);    
            }
            
            var lambda = Expression.Lambda<Func<T, bool>>(exprCall, parameter);
            predicate = predicate.Or(lambda);
        }
        */
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