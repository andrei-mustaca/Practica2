using CS.RequestResponse.Client;

namespace CS.Services.Interfaces;

public interface IClientService
{
    Task<CreateClientResponse> Create(CreateClientRequest request);
}