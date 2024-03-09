using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Contracts;
public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
{

    protected readonly HospitalContext _hospitalContext;
    protected RepositoryBase(HospitalContext hospitalContext)
    {
        _hospitalContext = hospitalContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        using (StreamWriter _logger = new("log.txt", true))
        {
            _logger.WriteLine($"{DateTime.UtcNow}. Добавление сущности типа {typeof(TEntity).Name}");
        }
        await _hospitalContext.Set<TEntity>().AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        using (StreamWriter _logger = new("log.txt", true))
        {
            _logger.WriteLine($"{DateTime.UtcNow}. Удаление сущности типа {typeof(TEntity).Name}");
        }
        _hospitalContext.Set<TEntity>().Remove(entity);
    }
    public void Update(TEntity entity)
    {
        using (StreamWriter _logger = new("log.txt", true))
        {
            _logger.WriteLine($"{DateTime.UtcNow}. Изменение сущности типа {typeof(TEntity).Name}");
        }
        _hospitalContext.Set<TEntity>().Update(entity);
    }

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, bool trackChanges)
    {
        var query = _hospitalContext.Set<TEntity>().Where(expression);

        if (!trackChanges)
        {
            return query.AsNoTracking();
        }

        return query;
    }

    public IQueryable<TEntity> GetAll(bool trackChanges)
    {
        var query = _hospitalContext.Set<TEntity>();

        if (!trackChanges)
        {
            return query.AsNoTracking();
        }

        return query;
    }
}
