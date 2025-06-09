using CS.Data.Enums;

namespace CS.Data.Models;

public class OrderHistory
{
    public Guid OrderId { get; set; }
    public HistoryEnum Status { get; set; }
    public DateTime OrderDate { get; set; }
    
    public Order Order { get; set; }
}