namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
}