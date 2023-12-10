using System.Linq.Expressions;

namespace OrgBloom.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null!);
    Task<int> SaveAsync();
}
