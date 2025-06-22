using CS.Data.Enums;

namespace CS.RequestResponse.Order;

public class CreatePaymentRequest
{
    public Guid OrderId { get; set; }
    public PaymentEnum Status { get; set; }
}