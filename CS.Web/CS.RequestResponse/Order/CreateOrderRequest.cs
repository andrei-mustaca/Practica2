namespace CS.RequestResponse.Order;

public class CreateOrderRequest
{
    public Guid ClientId { get; set; }
    public string DepartureName { get; set; }
    public string DestinationName { get; set; }
}