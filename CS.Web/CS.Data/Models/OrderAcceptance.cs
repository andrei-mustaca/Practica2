namespace CS.Data.Models;

public class OrderAcceptance
{
    public Guid OrderId { get; set; }
    public Guid CourierId { get; set; }
    public DateTime AcceptanceDate { get; set; }
    
    public Order Order { get; set; }
    public Courier Courier { get; set; }
}