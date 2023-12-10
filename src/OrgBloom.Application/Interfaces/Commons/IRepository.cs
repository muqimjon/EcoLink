using System.Linq.Expressions;

namespace OrgBloom.Application.Interfaces.Commons;

public interface IGoogleSheetsRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null!);
    Task<int> SaveAsync();
}
