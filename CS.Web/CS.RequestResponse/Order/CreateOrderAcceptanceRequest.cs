namespace CS.RequestResponse.Order;

public class CreateOrderAcceptanceRequest
{
    public Guid OrderId { get; set; }
    public Guid CourierId { get; set; }
}