namespace EmailSenderService.Configurations;

public class EmailSenderKafkaConsumerConfiguration
{
    public string Topic { get; set; }

    public string BootstrapServers { get; set; }

    public string GroupId { get; set; }
}