using CS.Data.Enums;
using CS.RequestResponse.EnumsRequestResponse;

namespace CS.RequestResponse.Order;

public class CreatePaymentRequest
{
    public Guid OrderId { get; set; }
    public PaymentEnum Status { get; set; }
}