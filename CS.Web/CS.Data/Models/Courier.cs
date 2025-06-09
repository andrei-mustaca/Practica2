namespace CS.Data.Models;

public class Courier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TelephoneNumber { get; set; }
    public decimal OrderPercent { get; set; } = 20;
    
    public List<OrderAcceptance> OrderAcceptances { get; set; }
}