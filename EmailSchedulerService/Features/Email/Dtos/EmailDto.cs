namespace EmailSchedulerService.Features.Email.Dtos;

public class EmailDto
{
    public int Id { get; set; }

    public string ReceiverEmail { get; set; }
    
    public string Message { get; set; }

    public DateTimeOffset DeliveryDate { get; set; }

    public bool IsSent { get; set; }
}