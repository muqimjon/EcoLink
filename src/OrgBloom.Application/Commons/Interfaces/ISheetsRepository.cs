namespace OrgBloom.Application.Commons.Interfaces;

public interface ISheetsRepository<TEntity>
{
    Task InsertAsync(TEntity entity);
}
