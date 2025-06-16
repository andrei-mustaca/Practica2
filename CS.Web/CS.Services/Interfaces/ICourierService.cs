

using CS.RequestResponse.Courier;

namespace CS.Services.Interfaces;

public interface ICourierService
{
    Task<CreateCourierResponse> CreateCourier(CreateCourierRequest request);
}