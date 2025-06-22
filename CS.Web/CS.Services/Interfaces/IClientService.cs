using CS.RequestResponse.Client;

namespace CS.Services.Interfaces;

public interface IClientService
{
    Task<CreateClientResponse> Create(CreateClientRequest request);
    Task<CreateClientResponse> UpdateName(UpdateNameRequest request);
    Task<CreateClientResponse> UpdateEmail(UpdateEmailRequest request);
}