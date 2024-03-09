using System.Linq.Expressions;

namespace Contracts;
public interface IRepositoryBase<TEntity>
{
    IQueryable<TEntity> GetAll(bool trackChanges);
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, bool trackChanges);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
