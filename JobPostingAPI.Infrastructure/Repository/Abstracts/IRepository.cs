using System.Linq.Expressions;

namespace JobPortal.Infrastructure.Repository.Abstracts;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(object id);
    TEntity GetById(object id);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(object id);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
}
