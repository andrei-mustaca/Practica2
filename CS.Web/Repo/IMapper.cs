namespace Repo;

public interface IMapper<TResponse,TRequest,TModels>
where TResponse : class
where TRequest : class
where TModels : class
{
    TModels MapToModel(TRequest request);
    TResponse MapToResponse(TModels model);
}