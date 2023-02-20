using System.Text.Json;
using Confluent.Kafka;
using Domain.Models;
using EmailSenderService.Configurations;
using Microsoft.Extensions.Options;

namespace EmailSenderService;

public class EmailDetailsConsumerHostedService : IHostedService
{
    private readonly EmailSenderKafkaConsumerConfiguration _emailSenderKafkaConsumerConfiguration;
    private CancellationTokenSource? _consumerCancellationTokenSource;

    public EmailDetailsConsumerHostedService(IOptions<EmailSenderKafkaConsumerConfiguration> options)
    {
        _emailSenderKafkaConsumerConfiguration = options.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = _emailSenderKafkaConsumerConfiguration.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = _emailSenderKafkaConsumerConfiguration.GroupId,
        };
        
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe(_emailSenderKafkaConsumerConfiguration.Topic);
        _consumerCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        try
        {
            while (!_consumerCancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    var consumedMessage = consumer.Consume(_consumerCancellationTokenSource.Token);
                    var emailDetail = JsonSerializer.Deserialize<EmailDetail>(consumedMessage.Message.Value);
                    Console.WriteLine(
                        $"{emailDetail!.ReceiverEmail} sent {emailDetail.Message}! {emailDetail.DeliveryDate.ToString()}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine(e.Error.Reason);
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumerCancellationTokenSource!.Cancel();
        
        _consumerCancellationTokenSource!.Dispose();
        
        return Task.CompletedTask;
    }
}