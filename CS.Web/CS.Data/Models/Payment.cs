using CS.Data.Enums;

namespace CS.Data.Models;

public class Payment
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentEnum Status { get; set; }
    public DateTime PaymentDate { get; set; }
    
    public Order Order { get; set; }
}