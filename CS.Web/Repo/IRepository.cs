namespace Repo;

public interface IRepository <TEntity> where TEntity:class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByKeysAsync(params object[] keys);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
}