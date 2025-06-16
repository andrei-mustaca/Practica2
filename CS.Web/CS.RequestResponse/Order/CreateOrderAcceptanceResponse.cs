namespace CS.RequestResponse.Order;

public class CreateOrderAcceptanceResponse : BaseResponce
{
    public Guid OrderId { get; set; }
    public Guid CourierId { get; set; }
    public DateTime AcceptanceDate { get; set; }

    public CreateOrderAcceptanceResponse() : base() { }
    public CreateOrderAcceptanceResponse(bool success,string message):base(success,message){}

    public CreateOrderAcceptanceResponse(bool success, string message, Guid orderId, Guid courierId, DateTime acceptanceDate)
        : base(success, message)
    {
        OrderId = orderId;
        CourierId = courierId;
        AcceptanceDate = acceptanceDate;
    }
}