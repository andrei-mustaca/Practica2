namespace CS.RequestResponse.Courier;

public class CreateCourierResponse:BaseResponce
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TelephoneNumber { get; set; }
    public decimal OrderPercent { get; set; }

    public CreateCourierResponse() : base() { }
    
    public CreateCourierResponse(bool success,string message):base(success,message){}

    public CreateCourierResponse(bool success, string message, Guid id, string name, string telephoneNumber, decimal orderPercent)
        : base(success, message)
    {
        Id = id;
        Name = name;
        TelephoneNumber = telephoneNumber;
        OrderPercent = orderPercent;
    }
}