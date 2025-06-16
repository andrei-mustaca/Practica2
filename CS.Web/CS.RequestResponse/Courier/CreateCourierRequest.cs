using System.ComponentModel;

namespace CS.RequestResponse.Courier;

public class CreateCourierRequest
{
    public string Name { get; set; }
    public string TelephoneNumber { get; set; }
    [DefaultValue(0.2)]
    public decimal OrderPercent { get; set; } = 0.2m;
}