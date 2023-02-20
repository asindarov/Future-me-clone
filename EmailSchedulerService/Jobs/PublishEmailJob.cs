using EmailSchedulerService.Broker;
using EmailSchedulerService.Features.Email.Services;
using Quartz;
using Shared.EmailDetailsExtensions;

namespace EmailSchedulerService.Jobs;

public class PublishEmailJob : IJob
{
    private readonly KafkaDependantProducer<string, string> _producer;
    private readonly IEmailManager _emailManager;

    public PublishEmailJob(KafkaDependantProducer<string, string> producer, IEmailManager emailManager)
    {
        _producer = producer;
        _emailManager = emailManager;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var notSentEmailDetails = await _emailManager.GetAllNotSentEmailDetails();
        
        foreach (var notSentEmailDetail in notSentEmailDetails)
        {
            if (notSentEmailDetail.DeliveryDate.Date == DateTime.Today)
            {
                var message = notSentEmailDetail.BuildEmailDetailMessage();
                
                await _producer.ProduceAsync("emails", message);

                await _emailManager.MarkAsSent(notSentEmailDetail);
                
                Console.WriteLine($"Hi from futureme.org, from {notSentEmailDetail.ReceiverEmail} you have sent {notSentEmailDetail.Message} to yourself!!!");
            }
        }
        Console.WriteLine("Job is executed!!!");
    }
}