namespace CS.RequestResponse.Order;

public class CreateOrderResponse:BaseResponce
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid DepartureId { get; set; }
    public Guid DestinationId { get; set; }
    
    public CreateOrderResponse():base(){}
    public CreateOrderResponse(bool success,string message):base(success,message){}

    public CreateOrderResponse(bool success,string message,Guid id,Guid clientId,Guid departureId,Guid destinationId) : base(success,message)
    {
        Id = id;
        ClientId = clientId;
        DepartureId = departureId;
        DestinationId = destinationId;
    }
}