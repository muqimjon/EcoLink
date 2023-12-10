using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrgBloom.Application.Interfaces;
using OrgBloom.Infrastructure.Contexts;

namespace OrgBloom.Infrastructure.Repositories;

public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : class
{
    public DbSet<T> table
    {
        get
        {
            return dbContext.Set<T>();
        }
    }

    public async Task InsertAsync(T entity)
    {
        await table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        table.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        table.Remove(entity);
    }

    public void Delete(Expression<Func<T, bool>> expression)
    {
        foreach (var entity in table.Where(expression))
            table.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression)
        => (await table.FirstOrDefaultAsync(expression))!;

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!)
        => expression != null ? table.Where(expression) : table;

    public Task<int> SaveAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
