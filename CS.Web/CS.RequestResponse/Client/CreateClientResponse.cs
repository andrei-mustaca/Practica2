namespace CS.RequestResponse.Client;

public class CreateClientResponse:BaseResponce
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }
    
    public CreateClientResponse():base(){}

    public CreateClientResponse(bool success,string message):base(success,message){}
    public CreateClientResponse(bool success,string message,Guid id,string name,string email,string telephoneNumber) : base(success,message)
    {
        Id = id;
        Name = name;
        Email = email;
        TelephoneNumber = telephoneNumber;
    }
}