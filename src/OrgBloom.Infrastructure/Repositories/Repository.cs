using System.Linq.Expressions;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    public Task InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> SelectAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }
}
