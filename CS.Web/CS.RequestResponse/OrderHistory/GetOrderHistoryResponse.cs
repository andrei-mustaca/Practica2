using CS.Data.Enums;
using CS.RequestResponse.EnumsRequestResponse;

namespace CS.RequestResponse.OrderHistory;

public class GetOrderHistoryResponse:BaseResponce
{
    public Guid OrderId { get; set; }
    public List<OrderHistories> Histories { get; set; }
    
    public GetOrderHistoryResponse():base(){}
    
    public GetOrderHistoryResponse(bool success,string message):base(success,message){}

    public GetOrderHistoryResponse(bool success,string message,Guid orderId,List<OrderHistories> histories) : base(success,message)
    {
        OrderId = orderId;
        Histories = histories;
    }
}

public class OrderHistories
{
    public Guid OrderId { get; set; }
    public HistoryEnum Status { get; set; }
    public DateTime Date { get; set; }
}