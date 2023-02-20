using Confluent.Kafka;

namespace EmailSchedulerService.Broker;

public class KafkaClientHandle: IDisposable
{
    private readonly IProducer<byte[], byte[]> _kafkaProducer;
    private const string Kafka = nameof(Kafka);

    public KafkaClientHandle(IConfiguration configuration)
    {
        var kafkaUrl = configuration.GetSection(key: Kafka).Value;
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = kafkaUrl,
        };

        _kafkaProducer = new ProducerBuilder<byte[], byte[]>(producerConfig).Build();
    }

    public Handle Handle
    {
        get => _kafkaProducer.Handle;
    }
    
    public void Dispose()
    {
        _kafkaProducer.Flush();
        _kafkaProducer.Dispose();
    }
}