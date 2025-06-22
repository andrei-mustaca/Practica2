using System.ComponentModel.Design.Serialization;
using CS.Data.Models;
using CS.RequestResponse;
using CS.RequestResponse.Client;
using CS.Services.Interfaces;
using CS.Services.Mapper;
using Repo;

namespace CS.Services;

public class ClientService:IClientService
{
    private readonly IRepository<Client> _clientRepository;
    private readonly IMapper<CreateClientResponse,CreateClientRequest,Client> _mapper;

    public ClientService(IRepository<Client> clientRepository,IMapper<CreateClientResponse,CreateClientRequest,Client> mapper)
    {
        _clientRepository=clientRepository;
        _mapper=mapper;
    }

    public async Task<CreateClientResponse> Create(CreateClientRequest request)
    {
        IEnumerable<Client> clients =await _clientRepository.GetAllAsync();
        foreach (var elem in clients)
        {
            if (elem.Email == request.Email||elem.TelephoneNumber==request.TelephoneNumber)
            {
                return new CreateClientResponse(false,"Ошибка, такая почта или номер телефон уже зарегистрированы");
            }
        }
        
        var client = _mapper.MapToModel(request);
        await _clientRepository.AddAsync(client);
        await _clientRepository.SaveChangesAsync();
        return _mapper.MapToResponse(client);
    }

    public async Task<CreateClientResponse> UpdateName(UpdateNameRequest request)
    {
        var client = await _clientRepository.GetByKeysAsync(request.ClientId);
        if (client == null)
            return new CreateClientResponse(false,"Клиент не найден");
        client.Name = request.Name;
        _clientRepository.Update(client);
        await _clientRepository.SaveChangesAsync();
        return _mapper.MapToResponse(client);
    }
    
    public async Task<CreateClientResponse> UpdateEmail(UpdateEmailRequest request)
    {
        var client = await _clientRepository.GetByKeysAsync(request.ClientId);
        if (client == null)
            return new CreateClientResponse(false,"Клиент не найден");
        client.Email = request.Email;
        _clientRepository.Update(client);
        await _clientRepository.SaveChangesAsync();
        return _mapper.MapToResponse(client);
    }
}