namespace CS.RequestResponse.Order;

public class GetClientOrdersResponse:BaseResponce
{
    public Guid ClientId { get; set; }
    public List<OrderSummary> Orders { get; set; }
    public GetClientOrdersResponse():base(){}
    public GetClientOrdersResponse(bool success,string message):base(success,message){}

    public GetClientOrdersResponse(bool success,string message,Guid clientId, List<OrderSummary> orders) : base(success,message)
    {
        ClientId = clientId;
        Orders = orders;
    }
}

public class OrderSummary
{
    public Guid Id { get; set; }
    public string DepartureName { get; set; }
    public string DestinationName { get; set; }
    public double Distance { get; set; }
}