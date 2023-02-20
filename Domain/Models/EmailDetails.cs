namespace Domain.Models;

public class EmailDetail
{
    public int Id { get; set; }

    public string ReceiverEmail { get; set; }
    
    public string Message { get; set; }

    public DateTimeOffset DeliveryDate { get; set; }

    public bool IsSent { get; set; }
}