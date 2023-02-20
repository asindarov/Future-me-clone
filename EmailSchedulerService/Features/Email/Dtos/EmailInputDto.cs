namespace EmailSchedulerService.Features.Email.Dtos;

public class EmailInputDto
{
    public string ReceiverEmail { get; set; }
    
    public string Message { get; set; }

    public DateTimeOffset DeliveryDate { get; set; }
}