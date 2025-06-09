namespace Repo;

public interface IRepository <TResponce,TRequest>
    where TResponce: class
    where TRequest: class
{
    Task<IEnumerable<TResponce>> GetAll();
    Task<TResponce> Get(Guid id);
    Task<BaseResponce> Create(TRequest item);
    Task<BaseResponce> Update(TRequest item);
    Task<BaseResponce> Delete(Guid id);
    Task<BaseResponce> Save();
}