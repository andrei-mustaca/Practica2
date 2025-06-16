using CS.RequestResponse.Order;

namespace CS.Services.Interfaces;

public interface IOrderService
{
    Task<CreateOrderResponse> Create(CreateOrderRequest request);
    Task<CreateOrderAcceptanceResponse> CreateAcceptance(CreateOrderAcceptanceRequest request);
    Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
    Task<GetClientOrdersResponse> GetClientOrders(GetClientOrdersRequest request);
    Task<GetCourierOrdersResponse> GetCourierOrders(GetCourierOrdersRequest request);
    Task<GetOrdersResponse> GetOrders(GetOrdersRequest request);
}