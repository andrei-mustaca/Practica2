namespace CS.Data.Models;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }
    public List<Order> Orders { get; set; }
}