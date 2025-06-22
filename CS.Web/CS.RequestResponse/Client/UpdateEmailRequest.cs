namespace CS.RequestResponse.Client;

public class UpdateEmailRequest
{
    public Guid ClientId { get; set; }
    public string Email { get; set; }
}