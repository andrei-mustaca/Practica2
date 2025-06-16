namespace CS.RequestResponse;

public class BaseResponce
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    public BaseResponce() {}

    public BaseResponce(bool success, string message)
    {
        Success = success;
        Message= message;
    }
}