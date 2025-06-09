using CS.Data;
using Microsoft.EntityFrameworkCore;

namespace Repo;

public class Repository <TResponse,TRequest,TModels>:IRepository<TResponse,TRequest> 
where TResponse : class
where TRequest : class
where TModels : class
{
    private readonly DataContext _options;
    private readonly DbSet<TModels> _dbSet;
    private readonly IMapper<TResponse,TRequest,TModels> _mapper;
    
    public Repository(DataContext options,IMapper<TResponse,TRequest,TModels> mapper)
    {
        _options = options;
        _dbSet=options.Set<TModels>();
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TResponse>> GetAll()
    {
        return _dbSet.AsEnumerable().Select(model=>_mapper.MapToResponse(model));
    }

    public async Task<TResponse> Get(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return _mapper.MapToResponse(entity);
    }

    public async Task<BaseResponce> Create(TRequest model)
    {
        await _dbSet.AddAsync(_mapper.MapToModel(model));
        return new BaseResponce(true,"Успешно создано");
    }

    public async Task<BaseResponce> Update(TRequest model)
    {
        _dbSet.Update(_mapper.MapToModel(model));
        return new BaseResponce(true,"Успешно обновлено");
    }

    public async Task<BaseResponce> Delete(Guid id)
    {
        TModels model = await _dbSet.FindAsync(id);
        _dbSet.Remove(model);
        return new BaseResponce(true,"Успешно удалено");
    }

    public async Task<BaseResponce> Save()
    {
        await _options.SaveChangesAsync();
        return new BaseResponce(true,"Успешно сохранено");
    }
}