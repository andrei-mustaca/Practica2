using CS.Data.Models;
using CS.RequestResponse.Client;

namespace CS.Services.Mapper;

public class ClientMapper: IMapper<CreateClientResponse,CreateClientRequest,Client>
{
    public Client MapToModel(CreateClientRequest request)
    {
        return new Client
        {
            Id=Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            TelephoneNumber = request.TelephoneNumber
        };
    }

    public CreateClientResponse MapToResponse(Client model)
    {
        return new CreateClientResponse
        {
            Success = true,
            Message = "Успешно создан",
            Id = model.Id,
            Name=model.Name,
            Email = model.Email,
            TelephoneNumber = model.TelephoneNumber
        };
    }
}