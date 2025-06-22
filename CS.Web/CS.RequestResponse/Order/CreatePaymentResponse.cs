using CS.Data.Enums;

namespace CS.RequestResponse.Order;

public class CreatePaymentResponse : BaseResponce
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentEnum Status { get; set; }
    public DateTime PaymentDate { get; set; }

    public CreatePaymentResponse() : base() { }
    public CreatePaymentResponse(bool success,string message):base(success,message){}

    public CreatePaymentResponse(bool success, string message, Guid orderId, decimal amount, PaymentEnum status, DateTime paymentDate)
        : base(success, message)
    {
        OrderId = orderId;
        Amount = amount;
        Status = status;
        PaymentDate = paymentDate;
    }
}