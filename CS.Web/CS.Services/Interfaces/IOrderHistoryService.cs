using CS.RequestResponse.OrderHistory;

namespace CS.Services.Interfaces;

public interface IOrderHistoryService
{
    Task<GetOrderHistoryResponse> GetOrderHistory(GetOrderHistoryRequest request);
}