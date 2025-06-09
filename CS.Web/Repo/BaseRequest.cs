namespace Repo;

public class BaseRequest
{
    public Guid Id { get; set; }
    public BaseRequest(){}

    public BaseRequest(Guid id)
    {
        Id = id;
    }
}