using CS.Data.Enums;

namespace CS.RequestResponse.Order;

public class GetCourierOrdersResponse : BaseResponce
{
    public Guid CourierId { get; set; }
    public List<OrderAcceptanceDto> AcceptedOrders { get; set; }

    public GetCourierOrdersResponse() : base() { }
    public GetCourierOrdersResponse(bool success,string message):base(success,message){}

    public GetCourierOrdersResponse(bool success, string message, Guid courierId, List<OrderAcceptanceDto> acceptedOrders)
        : base(success, message)
    {
        CourierId = courierId;
        AcceptedOrders = acceptedOrders;
    }
}

public class OrderAcceptanceDto
{
    public Guid OrderId { get; set; }
    public Guid CourierId { get; set; }
    public string DepartureName { get; set; }
    public string DestinationName { get; set; }
    public double Distance { get; set; }
    public HistoryEnum Status { get; set; }
    public DateTime AcceptanceDate { get; set; }
}