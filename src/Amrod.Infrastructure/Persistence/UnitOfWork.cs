using Microsoft.EntityFrameworkCore.Storage;

namespace Amrod.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDbContextTransaction _transaction;

    private readonly Dictionary<Type, object> _repositories = new();
    private bool _disposed;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _transaction = _context.Database.BeginTransaction();

    }
    
    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
        } 
    }
    
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(UnitOfWork));
        }

        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new Repository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository); 
        return repository;
    }

    private void CleanUp(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
                _repositories.Clear();
            }
            _disposed = true;
        }
    }
    
    public void Dispose()
    {
        CleanUp(true);
        GC.SuppressFinalize(this);
    }
}