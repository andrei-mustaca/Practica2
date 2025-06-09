namespace CS.Data.Models;

public class Route
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ParentId { get; set; }
    
    public List<Order> DepartureOrders { get; set; }
    public List<Order> DestinationOrders { get; set; }
    public Route Parent { get; set; }
    public List<Route> Children { get; set; }
}