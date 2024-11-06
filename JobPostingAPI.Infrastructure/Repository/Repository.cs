using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobPortal.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly JobPortalDbContext _dbContext;

    public Repository(JobPortalDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(object id)
    {
        TEntity entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null)
            _dbSet.Remove(entityToDelete);
    }
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        if (entities.Count() > 0)
            _dbSet.RemoveRange(entities);
    }


    public Task<IQueryable<TEntity>> FindWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var queryable = _dbSet.Where(predicate);
        foreach (var include in includes)
        {
            queryable = queryable.Include(include);
        }
        return Task.FromResult(queryable);
    }


    public TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }

}
