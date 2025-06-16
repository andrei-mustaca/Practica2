using CS.Data.Models;
using CS.RequestResponse.Courier;

namespace CS.Services.Mapper;

public class CourierMapper:IMapper<CreateCourierResponse,CreateCourierRequest,Courier>
{
    public Courier MapToModel(CreateCourierRequest request)
    {
        return new Courier
        {
            Id=Guid.NewGuid(),
            Name=request.Name,
            TelephoneNumber = request.TelephoneNumber,
            OrderPercent = request.OrderPercent
        };
    }

    public CreateCourierResponse MapToResponse(Courier courier)
    {
        return new CreateCourierResponse
        {
            Success = true,
            Message = "Успешно создан",
            Id=courier.Id,
            Name=courier.Name,
            TelephoneNumber=courier.TelephoneNumber,
            OrderPercent=courier.OrderPercent
        };
    }
}