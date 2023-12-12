using OrgBloom.Domain.Commons;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrgBloom.Infrastructure.Contexts;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Infrastructure.Repositories;

public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : Auditable
{
    public DbSet<T> Table
    {
        get
        {
            return dbContext.Set<T>();
        }
    }

    public async Task InsertAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Table.Entry(entity).State = EntityState.Modified;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public void Delete(Expression<Func<T, bool>> expression)
    {
        foreach (var entity in Table.Where(expression))
            Table.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression)
        => (await Table.FirstOrDefaultAsync(expression))!;

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!)
        => expression != null ? Table.Where(expression) : Table;

    public Task<int> SaveAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
