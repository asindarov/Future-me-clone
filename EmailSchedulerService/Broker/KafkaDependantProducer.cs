using Confluent.Kafka;

namespace EmailSchedulerService.Broker;

public class KafkaDependantProducer<TK, TV>
{
    private readonly IProducer<TK, TV> _kafkaHandle;

    public KafkaDependantProducer(KafkaClientHandle kafkaClientHandle)
    {
        _kafkaHandle = new DependentProducerBuilder<TK, TV>(handle: kafkaClientHandle.Handle).Build();
    }

    public Task ProduceAsync(string topic, Message<TK, TV> message) =>
        _kafkaHandle.ProduceAsync(topic, message);

    public void Produce(
        string topic,
        Message<TK, TV> message,
        Action<DeliveryReport<TK, TV>>? deliveryHandler = null) =>
        _kafkaHandle.Produce(topic, message, deliveryHandler);
    
    public void Flush(TimeSpan timeout) => this._kafkaHandle.Flush(timeout);
}