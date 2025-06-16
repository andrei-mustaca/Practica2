using CS.Data.Models;
using CS.RequestResponse.Courier;
using CS.Services.Interfaces;
using CS.Services.Mapper;
using Repo;

namespace CS.Services;

public class CourierService:ICourierService
{
    private readonly IRepository<Courier> _repository;
    private readonly IMapper<CreateCourierResponse, CreateCourierRequest, Courier> _mapper;

    public CourierService(IRepository<Courier> repository,IMapper<CreateCourierResponse,CreateCourierRequest,Courier> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateCourierResponse> CreateCourier(CreateCourierRequest request)
    {
        IEnumerable<Courier> couriers = await _repository.GetAllAsync();
        foreach (var elem in couriers)
        {
            if (elem.TelephoneNumber == request.TelephoneNumber)
            {
                return new CreateCourierResponse(false,"Такой номер телефона уже зарегистрирован");
            }
        }
        var courier = _mapper.MapToModel(request);
        await _repository.AddAsync(courier);
        await _repository.SaveChangesAsync();
        return _mapper.MapToResponse(courier);
    }
}