namespace CS.Data.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid DepartureId { get; set; }
    public Guid DestinationId { get; set; }
    
    public Client Client { get; set; }
    public List<OrderHistory> OrderHistories { get; set; }
    public Payment Payment { get; set; }
    public Route DepartureRoute { get; set; }
    public Route DestinationRoute { get; set; }
    public OrderAcceptance OrderAcceptance { get; set; }
}