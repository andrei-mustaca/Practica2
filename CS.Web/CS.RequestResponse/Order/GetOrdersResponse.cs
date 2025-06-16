namespace CS.RequestResponse.Order;

public class GetOrdersResponse : BaseResponce
{
    public Guid CourierId { get; set; }
    public List<OrderDto> AcceptedOrders { get; set; }

    public GetOrdersResponse() : base() { }
    public GetOrdersResponse(bool success,string message):base(success,message){}

    public GetOrdersResponse(bool success, string message, Guid courierId, List<OrderDto> acceptedOrders)
        : base(success, message)
    {
        CourierId = courierId;
        AcceptedOrders = acceptedOrders;
    }
}

public class OrderDto
{
    public Guid OrderId { get; set; }
    public string DepartureName { get; set; }
    public string DestinationName { get; set; }
    public double Distance { get; set; }
}